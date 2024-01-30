using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Yongshi InputControl;
    //public Button continueButton;
    //public Button exitButton;
    public Button[] buttons;
    private int selectedIndex = 0; // 当前选中的按钮索引
    public GameObject settingsWindow;
    //private void GameStop(InputAction.CallbackContext context)
    //{
    //    Time.timeScale = 0;
    //}
    //public void ContinueGame()
    //{
    //    Time.timeScale = 1;
    //}
    public void ExitGame() 
    {
        Debug.Log("我退出啦~~~~");
    }
    private void Awake()
    {
        InputControl = new Yongshi();
        InputControl.Gameplay.Choose.performed += OnChoosePerformed;
    }
    private void Start()
    {
        settingsWindow.SetActive(false);
        UpdateSelection();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsWindow();
        }
    }
    private void ToggleSettingsWindow()
    {
        HandleSettingsInput();
        if (settingsWindow.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        settingsWindow.SetActive(!settingsWindow.activeSelf);

        if (settingsWindow.activeSelf)
        {
            // 当设置窗口激活时，手动设置焦点到第一个按钮
            buttons[0].Select();
            HandleSettingsInput();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsWindow();
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0f; // 暂停游戏
    }
    void ResumeGame()
    {
        Time.timeScale = 1f; // 恢复游戏
    }
    void HandleSettingsInput()
    {
        // 使用WASD键进行上左下右选择
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    // 处理向上选择的逻辑
        //}
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
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    // 处理向下选择的逻辑
        //}
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
                ResumeGame();
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
}
