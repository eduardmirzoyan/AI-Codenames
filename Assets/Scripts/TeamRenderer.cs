using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class TeamRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI guessesLabel;
    [SerializeField] private TextMeshProUGUI wordsLeftLabel;

    [Header("Settings")]
    [SerializeField] private CardType teamColor;

    [Header("Debug")]
    [SerializeField] private Game game;
    [SerializeField] private Team team;

    private void Start()
    {
        GameEvents.instance.onGameInitialize += Initialize;
        GameEvents.instance.onThinkStart += OnThinkStart;
        GameEvents.instance.onGuessStart += OnGuessStart;
        GameEvents.instance.onGuessWord += OnGuess;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onGameInitialize -= Initialize;
        GameEvents.instance.onThinkStart -= OnThinkStart;
        GameEvents.instance.onGuessStart -= OnGuessStart;
        GameEvents.instance.onGuessWord += OnGuess;
    }

    public void Initialize(Game game)
    {
        this.game = game;
        switch (teamColor)
        {
            case CardType.Red:
                wordsLeftLabel.text = "" + game.redTeam.numCardsLeft;
                team = game.redTeam;
                break;
            case CardType.Blue:
                wordsLeftLabel.text = "" + game.blueTeam.numCardsLeft;
                team = game.blueTeam;
                break;
            default:
                throw new System.Exception("UNHANDLED TYPE: " + teamColor);
        }
    }

    private void OnThinkStart()
    {
        guessesLabel.text = "--";
    }

    private void OnGuessStart()
    {
        if (game.currentTeam == team)
        {
            if (game.currentTeam.numGuesses == -1)
            {
                guessesLabel.text = "INF (-1)";

            }
            else if (game.currentTeam.numGuesses == 0)
            {
                guessesLabel.text = "INF (0)";
            }
            else
            {
                guessesLabel.text = "" + team.numGuesses;
            }
        }
        else
        {
            guessesLabel.text = "--";
        }
    }

    private void OnGuess()
    {
        if (teamColor == game.redTeam.color)
        {
            guessesLabel.text = "" + game.redTeam.numGuesses;
            wordsLeftLabel.text = "" + game.redTeam.numCardsLeft;
        }
        else
        {
            guessesLabel.text = "" + game.blueTeam.numGuesses;
            wordsLeftLabel.text = "" + game.blueTeam.numCardsLeft;
        }
    }
}
