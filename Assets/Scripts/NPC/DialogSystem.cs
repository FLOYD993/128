using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI���")]
    public Text textLabel;
    public Image faceImage;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index; //�ָ�ÿһ��

    List<string> textList = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);
        index = 0;
    }
    private void OnEnable()
    {
        textLabel.text = textList[index]; //�ڿ�ʼ�������һ�仰
        index++;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && index == textList.Count) //�Ѿ��������һ��
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)) //������Eȷ��
        {
            textLabel.text = textList[index];
            index++;
        }
    }
    void GetTextFromFile(TextAsset file) //��ȡ�ļ�
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n'); //���س����и�
        foreach(var line in lineData)
        {
            textList.Add(line);
        }

    }
}
