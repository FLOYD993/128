using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.IO; //用于文件的读写
using LitJson;  //用于json数据的转换（需要自行导入LitJson.dll文件）

[System.Serializable]
public class Data
{
    public AccountFile accountFile;
    public PlayerFile playerFile;
    public EnemyFile[] enemyFiles;
}

public class SaveFile : MonoBehaviour
{
    public Data data;
    public List<EnemyFile> enemyFiles;

    //public List<int> activeMonsterPosition = new List<int>();
    //public List<int> activeMonsterType = new List<int>();

    //public int shootNum;
    //public int score;

    public void SaveByJson()
    {
        Debug.Log("Saving");

        //保存文件路径（美化代码）
        string directoryPath = Application.dataPath + "/StreamingFile";
        string filePath = directoryPath + "/byJson.json";        //文件类型可以时.json也可以是.txt

        if (!File.Exists(filePath))
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            StreamWriter swf = File.CreateText(filePath);
            swf.Close();
        }
        else
        {
            data = new Data();


            //保存当前游戏状态，创建save对象（CreateSaveGo函数根据保存数据需要编写）
            data = GetSaveFiles();

            //通过JsonMapper.ToJson方法将save对象中的数据转化为json字符串保存下来
            Debug.Log(data);

            // string saveJsonStrCurrHealth = JsonMapper.ToJson(saveFile.playerFile.currHealth);//using LitJson;
            //string saveJsonStrCurrHealth = JsonMapper.ToJson(200);//using LitJson;
            string saveJsonStrPosition = JsonMapper.ToJson(data);//using LitJson;

            //创建StreamWriter写入流（注意需要一个string参数代表路径）
            StreamWriter streamWriter = new StreamWriter(filePath);

            //将转化后的字符串写入目标文件
            //streamWriter.Write(saveJsonStrCurrHealth);
            streamWriter.Write(saveJsonStrPosition);


            //关闭StreamWriter
            streamWriter.Close();
        }
    }

    public void LoadByJson()
    {
        //string保存路径（美化代码）
        string directoryPath = Application.dataPath + "/StreamingFile";
        string filePath = directoryPath + "/byJson.json";

        if (!File.Exists(filePath))
        {
            SaveByJson();
        }
        else
        {
            //创建文件读取流（注意new对象时需要string类型参数代表路径）
            StreamReader streamReader = new StreamReader(filePath);

            //读取文件中所有的字符串（ReadToEnd代表读取到最后，即读取所有）
            string jsonStr = streamReader.ReadToEnd();

            //将读取到的字符串通过JsonMapper.ToObject转换为Save类型的对象（JsonMapper.ToObject<类型>(路径);）
            Data data = JsonMapper.ToObject<Data>(jsonStr);//using LitJson;

            //设置游戏状态，根据游戏需要自行编写
            LoadGame(data);
        }
    }

    private Data GetSaveFiles()
    {
        Data dataTemp = new Data();
        data.accountFile = new AccountFile();
        data.playerFile = new PlayerFile();
        data.enemyFiles = new EnemyFile[0];

        Debug.Log(PlayerController.Instance);

        dataTemp.accountFile = GameManager.Instance.SaveAccountFile();
        dataTemp.playerFile = PlayerController.Instance.GetComponent<PlayerFileManager>().SavePlayerFile();
        dataTemp.enemyFiles = EnemyManager.Instance.GetComponent<EnemyManager>().SaveEnemyFiles();

        return dataTemp;
    }

    private void LoadGame(Data data)
    {
        // 设置账户
        if (data.accountFile != null)
        {
            GameManager.Instance.LoadAccountFile(data.accountFile);
        }


        // 设置玩家
        if (data.playerFile != null)
        {
            PlayerController.Instance.GetComponent<PlayerFileManager>().LoadPlayerFile(data.playerFile);
        }

        // 设置怪物
        if (data.enemyFiles != null)
        {
            EnemyManager.Instance.LoadEnemyFiles(data.enemyFiles);
        }
    }
}
