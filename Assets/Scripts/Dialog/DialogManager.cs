using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox; //显示或者隐藏对话框
    public Text dialogText, nameText; //输出文字和对话者的名字

    [TextArea(1, 3)] //保证输入文字框不会显示成默认的一行
    public string[] dialogLines; //数组表示对话的内容
    [SerializeField] private int currentLine; //实时追踪当前对话窗口正在进行数组中哪一行、哪一个元素的文字内容输入
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
