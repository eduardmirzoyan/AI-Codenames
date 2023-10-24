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
        GameEvents.instance.onThinkStart += StarTimerThink;
        GameEvents.instance.onGuessStart += StarTimerGuess;
    }

    private void OnDestroy()
    {
        // Sub
        GameEvents.instance.onGameInitialize -= Initialize;
        GameEvents.instance.onThinkStart -= StarTimerThink;
        GameEvents.instance.onGuessStart -= StarTimerGuess;
    }

    private void Initialize(Game game)
    {
        this.game = game;
    }

    private void StarTimerThink(Team team)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateBarOverTime(game.settings.thinkTime));
    }

    private void StarTimerGuess(Team team)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateBarOverTime(game.settings.guessTime));
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
