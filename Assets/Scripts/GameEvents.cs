using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public Action<Game> onGameInitialize;

    public Action onViewStart;
    public Action onViewStop;

    public Action onThinkStart;
    public Action onThinkStop;

    public Action onGuessStart;
    public Action onGuessStop;

    public Action onGiveClue;
    public Action onGuessWord;

    public Action onGameOver;

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

    public void TriggerOnViewStart()
    {
        if (onViewStart != null)
        {
            onViewStart();
        }
    }

    public void TriggerOnViewStop()
    {
        if (onViewStop != null)
        {
            onViewStop();
        }
    }

    public void TriggerOnThinkStart()
    {
        if (onThinkStart != null)
        {
            onThinkStart();
        }
    }

    public void TriggerOnThinkStop()
    {
        if (onThinkStop != null)
        {
            onThinkStop();
        }
    }

    public void TriggerOnGuessStart()
    {
        if (onGuessStart != null)
        {
            onGuessStart();
        }
    }

    public void TriggerOnGuessStop()
    {
        if (onGuessStop != null)
        {
            onGuessStop();
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

    public void TriggerOnGameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }
}
