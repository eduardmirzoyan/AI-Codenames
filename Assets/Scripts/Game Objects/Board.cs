using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Board : ScriptableObject
{
    [Header("Data")]
    public Card[,] cards;

    public void Initialize(int width, int height, int numRed, int numBlue, int numBlack)
    {
        name = "Game Board";

        cards = new Card[width, height];

        List<Vector2Int> allPositions = new List<Vector2Int>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                allPositions.Add(new Vector2Int(i, j));
            }
        }

        // Set reds
        for (int i = 0; i < numRed; i++)
        {
            if (allPositions.Count == 0)
                return;

            int index = Random.Range(0, allPositions.Count);
            Vector2Int position = allPositions[index];

            var card = CreateInstance<Card>();
            card.Initialize($"Word [{position.x}, {position.y}]", CardType.Red);

            cards[position.x, position.y] = card;

            allPositions.RemoveAt(index);
        }

        // Set blues
        for (int i = 0; i < numBlue; i++)
        {
            if (allPositions.Count == 0)
                return;

            int index = Random.Range(0, allPositions.Count);
            Vector2Int position = allPositions[index];

            var card = CreateInstance<Card>();
            card.Initialize($"Word [{position.x}, {position.y}]", CardType.Blue);

            cards[position.x, position.y] = card;

            allPositions.RemoveAt(index);
        }

        // Set blacks
        for (int i = 0; i < numBlack; i++)
        {
            if (allPositions.Count == 0)
                return;

            int index = Random.Range(0, allPositions.Count);
            Vector2Int position = allPositions[index];

            var card = CreateInstance<Card>();
            card.Initialize($"Word [{position.x}, {position.y}]", CardType.Black);

            cards[position.x, position.y] = card;

            allPositions.RemoveAt(index);
        }

        // Set rest to neutral
        foreach (var position in allPositions)
        {
            var card = CreateInstance<Card>();
            card.Initialize($"Word [{position.x}, {position.y}]", CardType.Neutral);

            cards[position.x, position.y] = card;
        }
    }
}
