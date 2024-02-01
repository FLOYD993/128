using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Yongshi InputControl;

    //public Button[] buttons;
    //public Slider[] sliders;

    public Selectable[] settingOptions;

    private int selectedIndex; // 当前选中的按钮索引
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
            selectedIndex = 0;
            if (settingsWindow.activeSelf)
            {
                ResumeGame();
                isStop = false;
            }
            else
            {
                PauseGame();
                isStop=true;
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                // 处理向左选择的逻辑
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex =settingOptions.Length - 1;
                }
                UpdateSelection();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // 处理向右选择的逻辑
                selectedIndex++;
                if (selectedIndex >= settingOptions.Length)
                {
                    selectedIndex = 0;
                }
                UpdateSelection();
            }
            if (Input.GetKeyDown(KeyCode.D) && settingOptions[selectedIndex] is Slider)
                {
                    Slider selectedSlider = settingOptions[selectedIndex] as Slider;
                    if(selectedSlider != null)
                            selectedSlider.value += 6f;
                }
            if (Input.GetKeyDown(KeyCode.A) && settingOptions[selectedIndex] is Slider)
                {
                    Slider selectedSlider = settingOptions[selectedIndex] as Slider;
                    if(selectedSlider != null)
                            selectedSlider.value -= 6f;
                }
            // 使用J键进行确认
            if (Input.GetKeyDown(KeyCode.J) && settingOptions[selectedIndex] is not Slider)
            {
                // 处理确认按钮的逻辑
                ConfirmSelection();
            }
        }

    private void ConfirmSelection()
    {
        switch (selectedIndex)
        {
            case 3:
                // 确认按钮3时的逻辑
                settingsWindow.SetActive(false);
                ResumeGame();
                Invoke("SetStopState", 0.3f);
                break;
            case 4:
                // 确认按钮4时的逻辑
                ExitGame();
                break;
        }
    }
    void UpdateSelection()
    {
        Debug.Log(settingOptions[selectedIndex]);
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
