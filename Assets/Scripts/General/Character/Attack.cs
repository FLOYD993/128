using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;
    public void OnTriggerStay2D(Collider2D collider)
    {
        collider.GetComponent<Character>().TakeDamage(this);
    }
}
