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
    private int selectedIndex = 0; // ��ǰѡ�еİ�ť����
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
        Debug.Log("���˳���~~~~");
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
            // �����ô��ڼ���ʱ���ֶ����ý��㵽��һ����ť
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
        Time.timeScale = 0f; // ��ͣ��Ϸ
    }
    void ResumeGame()
    {
        Time.timeScale = 1f; // �ָ���Ϸ
    }
    void HandleSettingsInput()
    {
        // ʹ��WASD��������������ѡ��
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    // ��������ѡ����߼�
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            // ��������ѡ����߼�
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = buttons.Length - 1;
            }
            UpdateSelection();
        }
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    // ��������ѡ����߼�
        //}
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // ��������ѡ����߼�
            selectedIndex++;
            if (selectedIndex >= buttons.Length)
            {
                selectedIndex = 0;
            }
            UpdateSelection();
        }
        // ʹ��J������ȷ��
        if (Input.GetKeyDown(KeyCode.J))
        {
            // ����ȷ�ϰ�ť���߼�
            ConfirmSelection();
        }
    }
    private void ConfirmSelection()
    {
        switch (selectedIndex)
        {
            case 0:
                // ȷ�ϰ�ť1ʱ���߼�
                ResumeGame();
                break;
            case 1:
                // ȷ�ϰ�ť2ʱ���߼�
                ExitGame();
                break;
            // ����и���İ�ť�����Լ������case��֧
            default:
                Debug.LogWarning("Unhandled button selection");
                break;
        }
    }
    void UpdateSelection()
    {
        // ���°�ť����ۣ�����ʾ��ǰѡ�еİ�ť
        for (int i = 0; i < buttons.Length; i++)
        {
            Color color = (i == selectedIndex) ? Color.cyan : Color.white;
            buttons[i].image.color = color;
        }
    }
    private void OnChoosePerformed(InputAction.CallbackContext context)
    {
        // ����ȷ��ѡ����߼�
        ConfirmSelection();
    }
}
