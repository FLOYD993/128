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

    private int selectedIndex; // ��ǰѡ�еİ�ť����
    public GameObject settingsWindow;

    public bool isStop;
    private float m_timer = 0; //����ʹ�ü�ʱ��


    public void ExitGame() 
    {
        Debug.Log("���˳���~~~~");
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
        Time.timeScale = 0f; // ��ͣ��Ϸ
    }
    void ResumeGame()
    {
        Time.timeScale = 1f; // �ָ���Ϸ
        FindObjectOfType<PlayerController>().isAttack = false; //�Ż�ȷ����ṥ��һ��
    }
        void HandleSettingsInput()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // ��������ѡ����߼�
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex =settingOptions.Length - 1;
                }
                UpdateSelection();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // ��������ѡ����߼�
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
            // ʹ��J������ȷ��
            if (Input.GetKeyDown(KeyCode.J) && settingOptions[selectedIndex] is not Slider)
            {
                // ����ȷ�ϰ�ť���߼�
                ConfirmSelection();
            }
        }

    private void ConfirmSelection()
    {
        switch (selectedIndex)
        {
            case 3:
                // ȷ�ϰ�ť3ʱ���߼�
                settingsWindow.SetActive(false);
                ResumeGame();
                Invoke("SetStopState", 0.3f);
                break;
            case 4:
                // ȷ�ϰ�ť4ʱ���߼�
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
        // ����ȷ��ѡ����߼�
        ConfirmSelection();
    }
    private void SetStopState()
    {
        isStop = false;
    }
    
}
