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
    public bool isStop; //�ж���Ϸ�Ƿ���ͣ

    public Questable questable; //��˵���Ķ�����ί�����������

    //[TextArea(1,4)]
    //public string[] nextlines01;
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
        isStop = FindObjectOfType<GameController>().isStop;
        if(!isStop && isEntered && Input.GetKeyDown(KeyCode.E) && DialogManager.instance.dialogBox.activeInHierarchy == false)
        {
            Button.SetActive(false);
            DialogManager.instance.ShowDialog(lines);
        }
    }
}
