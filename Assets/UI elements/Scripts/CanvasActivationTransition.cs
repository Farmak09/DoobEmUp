using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElement
{
    public Image image;
    public TextMeshProUGUI text = new();
    public float alpha = new();

    public void UpdateAlpha(float newAlpha)
    {
        if (image == null)
            text.alpha = newAlpha * alpha;
        else
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha * alpha);
    }
}

public class CanvasActivationTransition : MonoBehaviour
{
    readonly List<UIElement> fadingUIElements = new();

    public float fadeTime = new();
    private float currentTime = 0;

    private Vector3 initialPosition = new();
    private Vector3 movementVector = new();

    public Vector3 finalPosition = new();
    public Vector3 finalScale = new();

    private bool fadeOut = false;

    void Start()
    {
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            UIElement elm = new() {
                image = image,
                alpha = image.color.a
            };

            fadingUIElements.Add(elm);
        }
        foreach(TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
        {
            UIElement elm = new() {
                text = text,
                alpha = text.alpha
            };

            fadingUIElements.Add(elm);
        }
        initialPosition = transform.position;
        movementVector = finalPosition - initialPosition;
    }

    void Update()
    {
        if(fadeOut)
        {
            if (currentTime > 0)
            {
                UpdateMovementData(false);
            }
            else
            {
                LockInPosition(false);
                fadeOut = false;
                gameObject.SetActive(false);
                currentTime = 0;
            }
        }
        else if(currentTime <= fadeTime)
        {
            UpdateMovementData(true);
        }
        else
        {
            LockInPosition(true);
        }

    }

    private void UpdateMovementData(bool isOpening)
    {
        FadeOutElements();
        MoveToTarget(isOpening);

        currentTime += Time.deltaTime * (isOpening ? 1 : -1);
    }

    private void FadeOutElements()
    {
        for (int i = 0; i < fadingUIElements.Count; i++)
        {
            fadingUIElements[i].UpdateAlpha(currentTime / fadeTime);
        }
    }

    private void MoveToTarget(bool isOpening)
    {
        transform.position += Time.deltaTime * (isOpening ? 1 : -1) * movementVector / fadeTime;
    }

    private void LockInPosition(bool isOpening)
    {
        transform.position = (isOpening ? finalPosition : initialPosition);
    }

    public void TurnOff(bool value)
    {
        fadeOut = value;
    }

    [EditorCools.Button]
    public void SetFinalPositionData()
    {
        finalPosition = transform.position;
        finalScale = transform.localScale;
    }
}
