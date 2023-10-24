using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        GameEvents.instance.onGameInitialize += RenderBoard;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onGameInitialize -= RenderBoard;
    }

    private void RenderBoard(Game game)
    {
        foreach (var card in game.board.cards)
        {
            Instantiate(cardPrefab, gridLayout.transform).GetComponent<CardRenderer>().Initialize(card);
        }
    }
}
