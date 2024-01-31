using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Yongshi InputControl;
    public Button[] buttons;
    private int selectedIndex = 0; // 当前选中的按钮索引
    public GameObject settingsWindow;

    public bool isStop;
    private float m_timer = 0; //函数使用计时器
    public void ExitGame() 
    {
        Debug.Log("我退出啦~~~~");
    }
    private void Awake()
    {
        InputControl = new Yongshi();
        InputControl.Gameplay.Choose.started += OnChoosePerformed;
    }
    private void Start()
    {
        settingsWindow.SetActive(false);
        UpdateSelection();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsWindow.activeSelf)
            {
                ResumeGame();
                isStop = false;
            }
            else
            {
                PauseGame();
                isStop=true;
                buttons[0].Select();
            }
            settingsWindow.SetActive(!settingsWindow.activeSelf);
        }
        if (settingsWindow.activeSelf)
        {
            HandleSettingsInput();
        }
    }
        void PauseGame()
        {
            Time.timeScale = 0f; // 暂停游戏
        }
        void ResumeGame()
        {
            Time.timeScale = 1f; // 恢复游戏
            FindObjectOfType<PlayerController>().isAttack = false; //优化确定后会攻击一下
        }
        void HandleSettingsInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                // 处理向左选择的逻辑
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = buttons.Length - 1;
                }
                UpdateSelection();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // 处理向右选择的逻辑
                selectedIndex++;
                if (selectedIndex >= buttons.Length)
                {
                    selectedIndex = 0;
                }
                UpdateSelection();
            }
            // 使用J键进行确认
            if (Input.GetKeyDown(KeyCode.J))
            {
                // 处理确认按钮的逻辑
                ConfirmSelection();
            }
        }

    private void ConfirmSelection()
    {
        switch (selectedIndex)
        {
            case 0:
                // 确认按钮1时的逻辑
                settingsWindow.SetActive(false);
                ResumeGame();
                Invoke("SetStopState", 0.3f);
                break;
            case 1:
                // 确认按钮2时的逻辑
                ExitGame();
                break;
            // 如果有更多的按钮，可以继续添加case分支
            default:
                Debug.LogWarning("Unhandled button selection");
                break;
        }
    }
    void UpdateSelection()
    {
        // 更新按钮的外观，以显示当前选中的按钮
        for (int i = 0; i < buttons.Length; i++)
        {
            Color color = (i == selectedIndex) ? Color.cyan : Color.white;
            buttons[i].image.color = color;
        }
    }
    private void OnChoosePerformed(InputAction.CallbackContext context)
    {
        // 处理确认选择的逻辑
        ConfirmSelection();
    }
    private void SetStopState()
    {
        isStop = false;
    }
}
