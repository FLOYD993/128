using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject Button;
    [SerializeField] private bool isEntered;

    [TextArea(1, 3)]
    public string[] lines;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Button.SetActive(true);
            isEntered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Button.SetActive(false);
            isEntered = false;
        }
    }
    private void Update()
    {
        if(isEntered && Input.GetKeyDown(KeyCode.E) && DialogManager.instance.dialogBox.activeInHierarchy == false)
        {
            Button.SetActive(false);
            DialogManager.instance.ShowDialog(lines);
        }
    }
}
