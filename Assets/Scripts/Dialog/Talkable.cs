using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talkable : MonoBehaviour
{
    public GameObject Button;
    [SerializeField] private bool isEntered;

    [TextArea(1, 3)]
    public string[] lines;

    public Questable questable; //可说话的对象有委派任务的能力
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Button.SetActive(true);
            isEntered = true;

            DialogManager.instance.currentQuestable = questable;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Button.SetActive(false);
            isEntered = false;

            DialogManager.instance.currentQuestable = null;
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
