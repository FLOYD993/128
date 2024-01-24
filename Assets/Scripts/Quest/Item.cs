using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Item instance;

    private bool isEntered = false;
    private void Update()
    {
       
    }
    public void IsGathering()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isEntered = true;
        }
    }
}
