using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 0_Main  -> 1_SelectFile: OnStartGameClick()
///         -> 2_Gameplay: OnSelectFile()
/// </summary>

public class SceneConrtoller : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Debug.Log(GameManager.Instance);
    }

    /// <summary>
    /// 从主页点击开始游戏，进入选择存档界面
    /// </summary>
    public void OnStartGameClick()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnQuitToMainMenu()
    {
        Debug.Log(GameManager.Instance);

        // 防止从游戏退出再进入timeScale未重置
        Time.timeScale = 1.0F;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void OnQuitToDesktop()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // 开始游戏
    public void OnSelectFile(int fileIndex)
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);

        if (!gameManager.GetComponent<FileInfo>().isSaved[fileIndex])           // 如果没有存档，先存档
        {
            gameManager.GetComponent<FileInfo>().isSaved[fileIndex] = true;
        }
        else                                                                    // 如果有存档，先读取存档
        {
            // 读档
            if (gameManager != null)
            {
                gameManager.LoadFiles();
            }
        }
    }

    public void OnPause(GameObject menu)
    {
        // 暂停
        Time.timeScale = 0.0F;

        // 开启选项菜单
        menu.SetActive(true);
    }

    public void OnResume(GameObject menu)
    {
        // 回到游戏
        Time.timeScale = 1.0F;

        // 关闭选项菜单
        menu.SetActive(false);

    }

    public void SaveFile()
    {
        // 存档
        if (gameManager != null)
        {
            gameManager.SaveFiles();
        }
    }

    public void SaveAccount()
    {
        GameManager.Instance.LoadFiles();
    }
}
