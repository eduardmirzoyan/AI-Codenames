using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadsUpInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI gameStateLabel;

    private void Start()
    {
        GameEvents.instance.onThinkStart += OnThinkStart;
        GameEvents.instance.onGuessStart += OnGuessStart;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onThinkStart -= OnThinkStart;
        GameEvents.instance.onGuessStart -= OnGuessStart;
    }

    private void OnThinkStart(Team team)
    {
        gameStateLabel.text = $"{team.cardType} Team: Give Clue";
    }

    private void OnGuessStart(Team team)
    {
        gameStateLabel.text = $"{team.cardType} Team: Guess";
    }
}

