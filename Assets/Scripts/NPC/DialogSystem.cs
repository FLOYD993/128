using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index; //分割每一行

    List<string> textList = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);
        index = 0;
    }
    private void OnEnable()
    {
        textLabel.text = textList[index]; //在开始就输出第一句话
        index++;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && index == textList.Count) //已经到了最后一行
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)) //交互键E确定
        {
            textLabel.text = textList[index];
            index++;
        }
    }
    void GetTextFromFile(TextAsset file) //读取文件
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n'); //按回车键切割
        foreach(var line in lineData)
        {
            textList.Add(line);
        }

    }
}
