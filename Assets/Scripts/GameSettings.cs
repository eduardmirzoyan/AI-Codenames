using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public int boardWidth;
    public int boardHeight;
    public int numRed;
    public int numBlue;
    public int numBlack;

    public float viewTime;
    public float thinkTime;
    public float guessTime;

    public bool isStrict;
}
