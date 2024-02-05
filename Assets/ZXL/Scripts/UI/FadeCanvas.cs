using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FadeCanvas : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public FadeEventSO fadeEventSO;
    public Image fadeImage;

    private void OnEnable()
    {
        fadeEventSO.OnEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEventSO.OnEventRaised -= OnFadeEvent;
    }

    private void OnFadeEvent(Color targetColor, float duration, bool isFadeIn)
    {
        fadeImage.DOBlendableColor(targetColor, duration);
    }
}
 