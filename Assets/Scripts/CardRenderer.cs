using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class CardRenderer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI wordLabel;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    [SerializeField] private Outline outline;

    [Header("Settings")]
    [SerializeField] private float flipDuration;
    [SerializeField] private Color neutralColor;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color blackColor;
    [SerializeField] private bool debugMode;

    [Header("Debug")]
    [SerializeField] private Card card;
    [SerializeField] private Color defaultColor;
    [SerializeField] private bool isRevealed;

    public void Initialize(Card card)
    {
        this.card = card;

        wordLabel.text = card.word;
        defaultColor = image.color;

        if (debugMode)
        {
            switch (card.type)
            {
                case CardType.Neutral:
                    wordLabel.color = neutralColor;
                    break;
                case CardType.Red:
                    wordLabel.color = redColor;
                    break;
                case CardType.Blue:
                    wordLabel.color = blueColor;
                    break;
                case CardType.Black:
                    wordLabel.color = blackColor;
                    break;
            }
        }

        gameObject.name = card.name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(FlipOverTime());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }

    private IEnumerator FlipOverTime()
    {
        // Prevent interactions
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        float elapsed = 0f;
        while (elapsed < flipDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 90, 0), elapsed / flipDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Change color
        if (isRevealed)
        {
            image.color = defaultColor;
        }
        else
        {
            switch (card.type)
            {
                case CardType.Neutral:
                    image.color = neutralColor;
                    break;
                case CardType.Red:
                    image.color = redColor;
                    break;
                case CardType.Blue:
                    image.color = blueColor;
                    break;
                case CardType.Black:
                    image.color = blackColor;
                    break;
            }
        }

        elapsed = 0f;
        while (elapsed < flipDuration)
        {
            transform.localEulerAngles = Vector3.Lerp(new Vector3(0, 90, 0), Vector3.zero, elapsed / flipDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Allow interactions
        transform.localEulerAngles = Vector3.zero;
        isRevealed = !isRevealed;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        // Select this card in Game Manager
    }
}
