using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBarController : MonoBehaviour
{
    private FSM manager;
    private Parameter parameter;

    public GameObject bloodBarAppearance;

    private void Start()
    {
        manager = GetComponentInParent<FSM>();
        parameter = manager.parameter;

        bloodBarAppearance.GetComponent<RectTransform>().localScale = new Vector3 (1F, 1F, 1F);
    }

    public void OnCharactorFlip()
    {
        switch (gameObject.GetComponent<RectTransform>().localScale.x)
        {
            case 1:
                gameObject.GetComponent<RectTransform>().localScale = new Vector3(-1F, 1F, 1F);
                break;
            case -1:
                gameObject.GetComponent<RectTransform>().localScale = new Vector3(1F, 1F, 1F);
                break;
        }

    }

    public void GetHit()
    {
        float scale = 1.0F * parameter.currentHealth / parameter.maxHealth;

        Debug.Log(scale);

        if (scale > 0)
            bloodBarAppearance.GetComponent<RectTransform>().localScale = new Vector3(scale, 1F, 1F);
        else
            bloodBarAppearance.GetComponent<RectTransform>().localScale = new Vector3(0F, 1F, 1F);
    }
}
