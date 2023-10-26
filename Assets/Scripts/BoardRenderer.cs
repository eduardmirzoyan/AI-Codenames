using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        GameEvents.instance.onGameInitialize += RenderBoard;
        GameEvents.instance.onGuessStart += AllowInteraction;
        GameEvents.instance.onGuessStop += PreventInteraction;
        GameEvents.instance.onGameOver += PreventInteraction;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onGameInitialize -= RenderBoard;
        GameEvents.instance.onGuessStart -= AllowInteraction;
        GameEvents.instance.onGuessStop -= PreventInteraction;
        GameEvents.instance.onGameOver -= PreventInteraction;
    }

    private void RenderBoard(Game game)
    {
        foreach (var card in game.board.cards)
        {
            Instantiate(cardPrefab, gridLayout.transform).GetComponent<CardRenderer>().Initialize(card);
        }
    }

    private void AllowInteraction()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void PreventInteraction()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
