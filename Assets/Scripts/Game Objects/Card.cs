using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Neutral, Red, Blue, Black }

public class Card : ScriptableObject
{
    public string word;
    public CardType type;

    public void Initialize(string word, CardType type)
    {
        this.word = word;
        this.type = type;

        name = $"{word} Card";
    }
}
