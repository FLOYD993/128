using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer > 6)
            Debug.Log(collision.gameObject.name);
    }
}
