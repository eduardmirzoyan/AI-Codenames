using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    public CardType cardType;
    public int numCardsLeft;
    public int numGuesses;
    public int numLeftover;

    public Team(CardType cardType, int numCards)
    {
        this.cardType = cardType;
        numCardsLeft = numCards;
        numGuesses = 0;
        numLeftover = 0;
    }
}
