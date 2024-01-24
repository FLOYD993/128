using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);

        if(collider.gameObject.layer == 10)
            collider.GetComponentInChildren<StateController>().GetHit(damage);
    }
}
