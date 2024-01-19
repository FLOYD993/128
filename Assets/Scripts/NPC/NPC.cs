using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject dialogUI;
    public GameObject signSprite;
    //private void Awake()
    //{
    //    sign = GetComponent<Sign>();
    //}
    private void Update()
    {
        if(signSprite.GetComponent<SpriteRenderer>().enabled && Input.GetKeyDown(KeyCode.E ))
        {
            dialogUI.SetActive(true);
            //Debug.Log("1");
        }
    }
}
