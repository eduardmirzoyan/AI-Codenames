using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public Action<Game> onGameInitialize;
    public Action<Team> onThinkStart;
    public Action<Team> onThinkStop;
    public Action<Team> onGuessStart;
    public Action<Team> onGuessStop;

    public Action onGiveClue;
    public Action onGuessWord;


    public static GameEvents instance;
    private void Awake()
    {
        // Singleton logic
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void TriggerOnGameIntialize(Game game)
    {
        if (onGameInitialize != null)
        {
            onGameInitialize(game);
        }
    }

    public void TriggerOnThinkStart(Team team)
    {
        if (onThinkStart != null)
        {
            onThinkStart(team);
        }
    }

    public void TriggerOnThinkStop(Team team)
    {
        if (onThinkStop != null)
        {
            onThinkStop(team);
        }
    }

    public void TriggerOnGuessStart(Team team)
    {
        if (onGuessStart != null)
        {
            onGuessStart(team);
        }
    }

    public void TriggerOnGuessStop(Team team)
    {
        if (onGuessStop != null)
        {
            onGuessStop(team);
        }
    }

    public void TriggerOnGiveClue()
    {
        if (onGiveClue != null)
        {
            onGiveClue();
        }
    }

    public void TriggerOnGuessWord()
    {
        if (onGuessWord != null)
        {
            onGuessWord();
        }
    }
}
