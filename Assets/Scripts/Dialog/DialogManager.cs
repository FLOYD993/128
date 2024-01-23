using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox; //��ʾ�������ضԻ���
    public Text dialogText, nameText; //������ֺͶԻ��ߵ�����

    [TextArea(1, 3)] //��֤�������ֿ򲻻���ʾ��Ĭ�ϵ�һ��
    public string[] dialogLines; //�����ʾ�Ի�������
    [SerializeField] private int currentLine; //ʵʱ׷�ٵ�ǰ�Ի��������ڽ�����������һ�С���һ��Ԫ�ص�������������
    private void Start()
    {
        dialogText.text = dialogLines[currentLine];
    }
    private void Update()
    {

        if(dialogBox.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            currentLine++;

            if(currentLine < dialogLines.Length) 
                dialogText.text = dialogLines[currentLine];
            else
                dialogBox.SetActive(false);
        }
    }
}
