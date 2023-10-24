using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn { Red, Blue }

public class Game : ScriptableObject
{
    public Team[] teams;
    public Board board;
    public Turn turn;
    public int teamIndex;

    public Team currentTeam
    {
        get
        {
            return teams[teamIndex];
        }
    }

    public void Initialize(GameSettings gameSettings)
    {
        int teamSize = System.Enum.GetValues(typeof(Turn)).Length;
        teams = new Team[teamSize];

        for (int i = 0; i < teamSize; i++)
        {
            var team = new Team(turn, gameSettings.numRed);
            teams[i] = team;

            turn++;
        }

        board = CreateInstance<Board>();
        board.Initialize(gameSettings.boardWidth, gameSettings.boardHeight, gameSettings.numRed, gameSettings.numBlue, gameSettings.numBlack);

        turn = Turn.Red;

        teamIndex = 0;

        name = "Game";
    }

    public void IncrementTeam()
    {
        teamIndex++;
        if (teamIndex >= teams.Length)
            teamIndex = 0;
    }
}
