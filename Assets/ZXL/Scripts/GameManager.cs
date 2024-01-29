using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountFile
{
    public bool[] isActive;
}

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 单例解决重复创建物体
    /// </summary>
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public bool AccountSate;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            _instance = this;
        }

        LoadFiles();
    }

    void Start()
    {
        //切换场景不会被销毁
        DontDestroyOnLoad(gameObject);
    }

    // 游戏存档
    public void SaveFiles()
    {
        GetComponent<SaveFile>().SaveByJson();
    }

    public void LoadFiles()
    {
        GetComponent<SaveFile>().LoadByJson();
    }


    public AccountFile SaveAccountFile()
    {
        AccountFile accountFileTemp = new AccountFile();

        accountFileTemp.isActive = GetComponent<FileInfo>().isSaved;

        return accountFileTemp;
    }

    public void LoadAccountFile(AccountFile saveFile)
    {
        if (saveFile == null) { return; }

        GetComponent<FileInfo>().isSaved = saveFile.isActive;
    }


}
