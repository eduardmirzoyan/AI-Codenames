using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameSettings gameSettings;
    public Color neutralColor;
    public Color redColor;
    public Color blueColor;
    public Color blackColor;

    [Header("Debug")]
    [SerializeField] private Game game;

    public static GameManager instance;
    private void Awake()
    {
        // Singleton logic
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        game = ScriptableObject.CreateInstance<Game>();
        game.Initialize(gameSettings);

        StartCoroutine(StartView());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Testing
            Clue(Random.Range(1, 10));
        }
    }

    private IEnumerator StartView()
    {
        yield return null;

        GameEvents.instance.TriggerOnGameIntialize(game);

        yield return null;

        print("Start Viewing");

        // Show Cards for spymasters
        GameEvents.instance.TriggerOnViewStart();

        yield return new WaitForSeconds(gameSettings.viewTime);

        yield return StopView();
    }

    private IEnumerator StopView()
    {
        print("Stop Viewing");

        // Flip cards back
        GameEvents.instance.TriggerOnViewStop();

        yield return new WaitForSeconds(0.5f);

        yield return StartThinking();
    }

    private IEnumerator StartThinking()
    {
        print("Start Thinking");

        GameEvents.instance.TriggerOnThinkStart();

        yield return new WaitForSeconds(gameSettings.thinkTime);

        // If clue was not given, then skip turn
        if (gameSettings.isStrict)
        {
            game.IncrementTeam();

            yield return StartThinking();
        }
    }

    private IEnumerator StopThinking()
    {
        print("Stop Thinking");

        GameEvents.instance.TriggerOnThinkStop();

        yield return null;

        yield return StartGuessing();
    }

    private IEnumerator StartGuessing()
    {
        print("Start Guessing");

        GameEvents.instance.TriggerOnGuessStart();

        yield return new WaitForSeconds(gameSettings.guessTime);

        if (gameSettings.isStrict)
            yield return StopGuessing();
    }

    private IEnumerator StopGuessing()
    {
        print("Stop Guessing");

        GameEvents.instance.TriggerOnGuessStop();

        yield return null;

        game.IncrementTeam();

        yield return StartThinking();
    }

    public void Clue(int count)
    {
        game.currentTeam.numGuesses = count;

        GameEvents.instance.TriggerOnGiveClue();

        StopAllCoroutines();
        StartCoroutine(StopThinking());
    }

    public void Guess(Vector2Int position)
    {
        var guessedCard = game.board.cards[position.x, position.y];
        var guessingTeam = game.currentTeam;

        switch (guessedCard.type)
        {
            case CardType.Black:

                print("Choose black");

                guessingTeam.numGuesses = 0;
                game.otherTeam.numCardsLeft = 0;

                GameEvents.instance.TriggerOnGuessWord();

                StopAllCoroutines();

                GameEvents.instance.TriggerOnGameOver();

                break;
            case CardType.Neutral:

                print("Choose tan");

                // Rest
                guessingTeam.numGuesses = 0;

                GameEvents.instance.TriggerOnGuessWord();

                // End guessing
                StopAllCoroutines();
                StartCoroutine(StopGuessing());

                break;
            case CardType.Red:

                game.redTeam.numCardsLeft--;
                guessingTeam.numGuesses--;

                GameEvents.instance.TriggerOnGuessWord();

                if (game.redTeam.numCardsLeft == 0)
                {
                    StopAllCoroutines();
                    GameEvents.instance.TriggerOnGameOver();
                    return;
                }

                if (guessingTeam.color != CardType.Red)
                {
                    guessingTeam.numGuesses = 0;

                    StopAllCoroutines();
                    StartCoroutine(StopGuessing());
                }

                if (guessingTeam.numGuesses == 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(StopGuessing());
                }

                break;
            case CardType.Blue:

                game.blueTeam.numCardsLeft--;
                guessingTeam.numGuesses--;

                GameEvents.instance.TriggerOnGuessWord();

                if (game.blueTeam.numCardsLeft == 0)
                {
                    StopAllCoroutines();
                    GameEvents.instance.TriggerOnGameOver();
                    return;
                }

                if (guessingTeam.color != CardType.Blue)
                {
                    guessingTeam.numGuesses = 0;

                    StopAllCoroutines();
                    StartCoroutine(StopGuessing());
                }

                if (guessingTeam.numGuesses == 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(StopGuessing());
                }

                break;
        }
    }
}
