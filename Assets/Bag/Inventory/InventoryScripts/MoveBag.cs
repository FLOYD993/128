using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour, IDragHandler
{
    //获得canvas桌布，可移动的空间范围
    public Canvas canvas;
    RectTransform currentRect;

    public void OnDrag(PointerEventData eventData)
    {
       currentRect.anchoredPosition += eventData.delta;
    }

    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
}