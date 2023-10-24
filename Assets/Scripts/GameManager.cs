using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameSettings gameSettings;

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

        GameEvents.instance.TriggerOnGameStart(game.board);

        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        print("Start Delay");

        yield return new WaitForSeconds(gameSettings.delayTime);

        yield return StartThinking();
    }

    private IEnumerator StartThinking()
    {
        print("Start Thinking");

        GameEvents.instance.TriggerOnThinkStart(game.currentTeam);

        yield return new WaitForSeconds(gameSettings.thinkTime);

        yield return StopThinking();
    }

    private IEnumerator StopThinking()
    {
        print("Stop Thinking");

        GameEvents.instance.TriggerOnThinkStop(game.currentTeam);

        yield return new WaitForSeconds(gameSettings.thinkTime);

        yield return StartGuessing();
    }

    private IEnumerator StartGuessing()
    {
        print("Start Guessing");

        GameEvents.instance.TriggerOnGuessStart(game.currentTeam);

        yield return new WaitForSeconds(gameSettings.guessTime);

        yield return StopGuessing();
    }

    private IEnumerator StopGuessing()
    {
        print("Stop Guessing");

        GameEvents.instance.TriggerOnGuessStop(game.currentTeam);

        yield return new WaitForSeconds(gameSettings.guessTime);

        game.IncrementTeam();

        yield return StartThinking();
    }

    public void Clue(int count)
    {
        game.currentTeam.numGuesses = count;

        StopAllCoroutines();
        StartCoroutine(StartGuessing());
    }

    public void Guess(Vector2Int position)
    {
        var guessedCard = game.board.cards[position.x, position.y];
        if (guessedCard) // Correct choice
        {
            game.currentTeam.numCardsLeft--;
            game.currentTeam.numGuesses--;
        }
        else // Wrong choice
        {
            game.currentTeam.numGuesses = 0;

            StopAllCoroutines();
            StartCoroutine(StartThinking());
        }
    }
}
