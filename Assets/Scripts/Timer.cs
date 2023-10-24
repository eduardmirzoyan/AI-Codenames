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

    private void Start()
    {
        // Sub
        GameEvents.instance.onThinkStart += StartTimer;
        GameEvents.instance.onGuessStart += StartTimer;
    }

    private void OnDestroy()
    {
        // Sub
        GameEvents.instance.onThinkStart -= StartTimer;
        GameEvents.instance.onGuessStart -= StartTimer;
    }

    private void StartTimer(Team team)
    {
        StartCoroutine(UpdateBarOverTime());
    }

    private IEnumerator UpdateBarOverTime()
    {
        float duration = 1f;

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
