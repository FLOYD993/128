using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance; //����ģʽ��ͨ���ؼ���static���������ֻ���ɶ����һ��ʵ����ȷ���ű��ڳ����е�Ψһ��
    [Header("�Ի�������")]
    public Sprite sisterImage;
    public Sprite pa_rabbitImage;
    public Sprite brotherImage;
    public Image currentImage;

    public GameObject dialogBox; //��ʾ�������ضԻ���
    public Text dialogText, nameText; //������ֺͶԻ��ߵ�����

    [TextArea(1, 3)] //��֤�������ֿ򲻻���ʾ��Ĭ�ϵ�һ��
    public string[] dialogLines; //�����ʾ�Ի�������

    //д��[SerializeField]�ſ��Խ�private������Inspector�����пɼ�
    [SerializeField] private int currentLine; //ʵʱ׷�ٵ�ǰ�Ի��������ڽ�����������һ�С���һ��Ԫ�ص�������������
    [SerializeField] private float textSpeed; //���ֹ����ٶ�

    private bool isScrolling; //�Ƿ�����������
    private bool isStop; //�ж��Ƿ���ͣ

    public Questable currentQuestable; //��ǰ����˵���Ķ�����ʲô����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        dialogText.text = dialogLines[currentLine];
    }
    private void Update()
    {
        isStop = FindObjectOfType<GameController>().isStop;
        //�Ի�����
        if(dialogBox.activeInHierarchy && !isStop)
        {
            FindObjectOfType<PlayerController>().isTalk = true;
            FindObjectOfType<PlayerController>().rb.velocity = new Vector2(0f, FindObjectOfType<PlayerController>().rb.velocity.y);
            if (Input.GetKeyDown(KeyCode.J) && dialogText.text == dialogLines[currentLine])
            {
                if (!isScrolling) //ֻ���ı�ȫ��������˲ſ��Խ�����һ��
                {
                    currentLine++;

                    if (currentLine < dialogLines.Length)
                    {
                        CheckName();
                        //dialogText.text = dialogLines[currentLine]; //һ��һ�г���
                        StartCoroutine(ScrollingText()); //һ��һ������
                    }
                    else
                    {
                        dialogBox.SetActive(false);
                        FindObjectOfType<PlayerController>().isTalk = false;
                        if (currentQuestable == null) //����������NPC����ί�����������
                        {
                            //return;
                            Debug.Log("û������");
                        }
                        else
                        {
                            currentQuestable.DelegateQuest(); //��ɶԻ�֮��ί������
                            QuestManager.instance.UpdateQuestList(); //��������UI�б�
                        }
                    }
                }
            }
        }

    }
    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;
        currentLine = 0;  //�����ӵ�һ�仰��ʼ
        CheckName();

        //dialogText.text = dialogLines[currentLine]; //һ��һ�г���
        StartCoroutine(ScrollingText()); //һ��һ������

        dialogBox.SetActive(true); //����Ի�����
        FindObjectOfType<PlayerController>().isTalk = true;
    }
    private void CheckName()
    {
        if (dialogLines[currentLine].StartsWith("-"))
        {
            //����˵���˵�����
            nameText.text = dialogLines[currentLine].Replace("-","");
            //����˵���˵�����
            if (nameText.text == "���")
            {
                currentImage.sprite = sisterImage;
            }
            else if(nameText.text == "�ö�ү")
            {
                currentImage.sprite = pa_rabbitImage;
            }
            else if (nameText.text == "�ܵ�")
            {
                currentImage.sprite = brotherImage;
            }
            currentLine++;
        }
    }
    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogText.text = "";

        foreach(char letter in dialogLines[currentLine].ToCharArray()) //ToCharArray��ֵ�ַ��������е��ַ���һ��ͳһ���������
        {
            dialogText.text += letter; //�����ʾ
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }
}
