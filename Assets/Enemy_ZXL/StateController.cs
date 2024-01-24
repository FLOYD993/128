using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StateController : MonoBehaviour
{
    private FSM manager;
    private Parameter parameter;

    private void Start()
    {
        manager = GetComponentInParent<FSM>();
        parameter = manager.parameter;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + "Enter");
        if (collision.CompareTag("Player"))
        {
            parameter.target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name + "Exit");

        if (collision.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }

    public void GetHit(int damage)
    {
        parameter.currentHealth -= damage;
        parameter.isHit = true;
    }
}
