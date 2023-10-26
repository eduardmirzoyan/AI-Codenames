using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Neutral, Red, Blue, Black }

public class Card : ScriptableObject
{
    public string word;
    public CardType type;
    public Vector2Int position;

    public void Initialize(string word, CardType type, Vector2Int position)
    {
        this.word = word;
        this.type = type;
        this.position = position;

        name = $"{word} Card";
    }
}
