using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    }

    private void OnDestroy()
    {
        GameEvents.instance.onGameInitialize -= Initialize;
        GameEvents.instance.onThinkStart -= OnThinkStart;
        GameEvents.instance.onGuessStart -= OnGuessStart;
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

    private void OnThinkStart(Team team)
    {
        guessesLabel.text = "--";
    }

    private void OnGuessStart(Team team)
    {
        if (this.team == team)
        {
            guessesLabel.text = "" + team.numGuesses;
        }
        else
        {
            guessesLabel.text = "--";
        }
    }
}
