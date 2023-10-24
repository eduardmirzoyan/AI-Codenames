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
        GameEvents.instance.onGameStart += RenderBoard;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onGameStart -= RenderBoard;
    }

    private void RenderBoard(Board board)
    {
        foreach (var card in board.cards)
        {
            Instantiate(cardPrefab, gridLayout.transform).GetComponent<CardRenderer>().Initialize(card);
        }
    }
}
