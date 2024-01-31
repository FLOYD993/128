using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance; //单例模式，通过关键字static和其他语句只生成对象的一个实例，确保脚本在场景中的唯一性
    [Header("对话者立绘")]
    public Sprite sisterImage;
    public Sprite pa_rabbitImage;
    public Sprite brotherImage;
    public Image currentImage;

    public GameObject dialogBox; //显示或者隐藏对话框
    public Text dialogText, nameText; //输出文字和对话者的名字

    [TextArea(1, 3)] //保证输入文字框不会显示成默认的一行
    public string[] dialogLines; //数组表示对话的内容

    //写了[SerializeField]才可以将private变量在Inspector窗口中可见
    [SerializeField] private int currentLine; //实时追踪当前对话窗口正在进行数组中哪一行、哪一个元素的文字内容输入
    [SerializeField] private float textSpeed; //数字滚动速度

    private bool isScrolling; //是否正在输出语句
    private bool isStop; //判断是否暂停

    public Questable currentQuestable; //当前正在说话的对象有什么任务

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
        //对话更新
        if(dialogBox.activeInHierarchy && !isStop)
        {
            FindObjectOfType<PlayerController>().isTalk = true;
            FindObjectOfType<PlayerController>().rb.velocity = new Vector2(0f, FindObjectOfType<PlayerController>().rb.velocity.y);
            if (Input.GetKeyDown(KeyCode.J) && dialogText.text == dialogLines[currentLine])
            {
                if (!isScrolling) //只有文本全部输出完了才可以进行下一句
                {
                    currentLine++;

                    if (currentLine < dialogLines.Length)
                    {
                        CheckName();
                        //dialogText.text = dialogLines[currentLine]; //一行一行出现
                        StartCoroutine(ScrollingText()); //一个一个出现
                    }
                    else
                    {
                        dialogBox.SetActive(false);
                        FindObjectOfType<PlayerController>().isTalk = false;
                        if (currentQuestable == null) //并不是所有NPC都有委派任务的能力
                        {
                            //return;
                            Debug.Log("没有任务");
                        }
                        else
                        {
                            currentQuestable.DelegateQuest(); //完成对话之后委派任务
                            QuestManager.instance.UpdateQuestList(); //更新任务UI列表
                        }
                    }
                }
            }
        }

    }
    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;
        currentLine = 0;  //索引从第一句话开始
        CheckName();

        //dialogText.text = dialogLines[currentLine]; //一行一行出现
        StartCoroutine(ScrollingText()); //一个一个出现

        dialogBox.SetActive(true); //激活对话窗口
        FindObjectOfType<PlayerController>().isTalk = true;
    }
    private void CheckName()
    {
        if (dialogLines[currentLine].StartsWith("-"))
        {
            //更新说话人的名字
            nameText.text = dialogLines[currentLine].Replace("-","");
            //更新说话人的立绘
            if (nameText.text == "姐姐")
            {
                currentImage.sprite = sisterImage;
            }
            else if(nameText.text == "兔儿爷")
            {
                currentImage.sprite = pa_rabbitImage;
            }
            else if (nameText.text == "弟弟")
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

        foreach(char letter in dialogLines[currentLine].ToCharArray()) //ToCharArray赋值字符串中所有的字符到一个统一码的数组中
        {
            dialogText.text += letter; //逐个显示
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }
}
