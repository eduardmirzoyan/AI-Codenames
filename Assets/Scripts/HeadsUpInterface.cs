using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadsUpInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI gameStateLabel;
    [SerializeField] private Game game;

    private void Start()
    {
        GameEvents.instance.onGameInitialize += Initialize;

        GameEvents.instance.onViewStop += OnViewStart;
        GameEvents.instance.onThinkStart += OnThinkStart;
        GameEvents.instance.onGuessStart += OnGuessStart;
        GameEvents.instance.onGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onGameInitialize -= Initialize;

        GameEvents.instance.onViewStop -= OnViewStart;
        GameEvents.instance.onThinkStart -= OnThinkStart;
        GameEvents.instance.onGuessStart -= OnGuessStart;
        GameEvents.instance.onGameOver -= OnGameOver;
    }

    private void Initialize(Game game)
    {
        this.game = game;
    }

    private void OnViewStart()
    {
        gameStateLabel.text = $"View Words...";
    }

    private void OnThinkStart()
    {
        gameStateLabel.text = $"{game.currentTeam.color} Team: Give Clue";
    }

    private void OnGuessStart()
    {
        gameStateLabel.text = $"{game.currentTeam.color} Team: Guess";
    }

    private void OnGameOver()
    {
        if (game.redTeam.numCardsLeft == 0)
        {
            gameStateLabel.text = "RED Wins!";
        }
        else
        {
            gameStateLabel.text = "BLUE Wins!";
        }
    }
}

