using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn { Red, Blue }

public class Game : ScriptableObject
{
    public GameSettings settings;
    public Team blueTeam;
    public Team redTeam;
    public Board board;

    public bool isRedTurn;
    public Team currentTeam
    {
        get
        {
            return isRedTurn ? redTeam : blueTeam;
        }
    }
    public Team otherTeam
    {
        get
        {
            return isRedTurn ? blueTeam : redTeam;
        }
    }

    public void Initialize(GameSettings gameSettings)
    {
        settings = gameSettings;

        redTeam = new Team(CardType.Red, gameSettings.numRed);
        blueTeam = new Team(CardType.Blue, gameSettings.numBlue);

        board = CreateInstance<Board>();
        board.Initialize(gameSettings.boardWidth, gameSettings.boardHeight, gameSettings.numRed, gameSettings.numBlue, gameSettings.numBlack);

        isRedTurn = false;

        name = "Game";
    }

    public void IncrementTeam()
    {
        isRedTurn = !isRedTurn;
    }
}
