using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private Animator anim;
    public GameObject signSprite;
    private bool canPress;
    public Transform playerTrans;

    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
    }
    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            canPress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }
}
