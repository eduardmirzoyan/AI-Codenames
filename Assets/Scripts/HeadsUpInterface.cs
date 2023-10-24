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
        GameEvents.instance.onThinkStart += OnTurnStart;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onThinkStart -= OnTurnStart;
    }

    private void OnTurnStart(Team team)
    {
        switch (team.turn)
        {
            case Turn.Red:
                gameStateLabel.text = "Red Turn";
                break;
            case Turn.Blue:
                gameStateLabel.text = "Blue Turn";
                break;
        }
    }
}

