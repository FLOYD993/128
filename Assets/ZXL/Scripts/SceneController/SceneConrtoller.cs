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
    /// ����ҳ�����ʼ��Ϸ������ѡ��浵����
    /// </summary>
    public void OnStartGameClick()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnQuitToMainMenu()
    {
        Debug.Log(GameManager.Instance);

        // ��ֹ����Ϸ�˳��ٽ���timeScaleδ����
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

    // ��ʼ��Ϸ
    public void OnSelectFile(int fileIndex)
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);

        if (!gameManager.GetComponent<FileInfo>().isSaved[fileIndex])           // ���û�д浵���ȴ浵
        {
            gameManager.GetComponent<FileInfo>().isSaved[fileIndex] = true;
        }
        else                                                                    // ����д浵���ȶ�ȡ�浵
        {
            // ����
            if (gameManager != null)
            {
                gameManager.LoadFiles();
            }
        }
    }

    public void OnPause(GameObject menu)
    {
        // ��ͣ
        Time.timeScale = 0.0F;

        // ����ѡ��˵�
        menu.SetActive(true);
    }

    public void OnResume(GameObject menu)
    {
        // �ص���Ϸ
        Time.timeScale = 1.0F;

        // �ر�ѡ��˵�
        menu.SetActive(false);

    }

    public void SaveFile()
    {
        // �浵
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
