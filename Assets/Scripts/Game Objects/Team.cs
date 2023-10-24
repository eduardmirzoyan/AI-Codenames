using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    public Turn turn;
    public int numCardsLeft;
    public int numGuesses;
    public int numLeftover;

    public Team(Turn turn, int numCards)
    {
        this.turn = turn;
        numCardsLeft = numCards;
        numGuesses = 0;
        numLeftover = 0;
    }
}
