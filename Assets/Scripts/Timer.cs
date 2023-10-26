using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI timeLabel;

    [Header("Data")]
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [Header("Debug")]
    [SerializeField] private Game game;

    private void Start()
    {
        // Sub
        GameEvents.instance.onGameInitialize += Initialize;

        GameEvents.instance.onViewStart += OnViewStart;
        GameEvents.instance.onThinkStart += StartTimerThink;
        GameEvents.instance.onThinkStop += StopTimerThink;
        GameEvents.instance.onGuessStart += StartTimerGuess;
        GameEvents.instance.onGuessStop += StopTimerGuess;
        GameEvents.instance.onGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        // Unsub
        GameEvents.instance.onGameInitialize -= Initialize;

        GameEvents.instance.onViewStart -= OnViewStart;
        GameEvents.instance.onThinkStart -= StartTimerThink;
        GameEvents.instance.onThinkStop -= StopTimerThink;
        GameEvents.instance.onGuessStart -= StartTimerGuess;
        GameEvents.instance.onGuessStop -= StopTimerGuess;
        GameEvents.instance.onGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
        fillImage.fillAmount = 1f;
        fillImage.color = startColor;
        timeLabel.text = "--";
    }

    private void Initialize(Game game)
    {
        this.game = game;
    }

    private void OnViewStart()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateBarOverTime(game.settings.viewTime));
    }

    private void StartTimerThink()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateBarOverTime(game.settings.thinkTime));
    }

    private void StopTimerThink()
    {
        StopAllCoroutines();
        fillImage.fillAmount = 0f;
    }

    private void StartTimerGuess()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateBarOverTime(game.settings.guessTime));
    }

    private void StopTimerGuess()
    {
        StopAllCoroutines();
        fillImage.fillAmount = 0f;
    }

    private IEnumerator UpdateBarOverTime(float duration)
    {
        float remaining = duration;
        while (remaining > 0)
        {
            // Goes from 0 -> 1
            float ratio = remaining / duration;

            // Set fill amount
            fillImage.fillAmount = ratio;

            // Change color based on fill
            fillImage.color = Color.Lerp(endColor, startColor, ratio);

            int rem = (int)remaining;
            // Change numerical display
            timeLabel.text = "" + rem;

            remaining -= Time.deltaTime;
            yield return null;
        }
    }
}
