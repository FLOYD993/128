using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IIntercatable
{
    public SpriteRenderer spriteRenderer;

    public GameObject lightObj;
    public Sprite darkSprite;
    public Sprite lightSprite;

    [Header("¹ã²¥")]
    public VoidEventSO SaveGameEvent;

    private bool isDone;

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? lightSprite : darkSprite;
        lightObj.SetActive(isDone);
    }

    public void TriggerAction()
    {
        if(isDone) { return; }

        isDone = true;
        spriteRenderer.sprite = lightSprite;
        lightObj.SetActive(true);

        SaveGameEvent.RaiseEvent();


        this.gameObject.tag = "Untagged";
    }
}
