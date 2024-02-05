using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private Yongshi playerInput;

    private bool canPress;

    private IIntercatable interactTarget;

    private void Awake()
    {
        playerInput = new Yongshi();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        playerInput.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnDisable()
    {
        canPress = false;
        playerInput.Gameplay.Confirm.started -= OnConfirm;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;

            interactTarget = collision.GetComponent<IIntercatable>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if(canPress)
        {
            interactTarget.TriggerAction();
        }
    }
}
