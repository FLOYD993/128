using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.IO; //�����ļ��Ķ�д
using LitJson;  //����json���ݵ�ת������Ҫ���е���LitJson.dll�ļ���

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

        //�����ļ�·�����������룩
        string directoryPath = Application.dataPath + "/StreamingFile";
        string filePath = directoryPath + "/byJson.json";        //�ļ����Ϳ���ʱ.jsonҲ������.txt

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


            //���浱ǰ��Ϸ״̬������save����CreateSaveGo�������ݱ���������Ҫ��д��
            data = GetSaveFiles();

            //ͨ��JsonMapper.ToJson������save�����е�����ת��Ϊjson�ַ�����������
            Debug.Log(data);

            // string saveJsonStrCurrHealth = JsonMapper.ToJson(saveFile.playerFile.currHealth);//using LitJson;
            //string saveJsonStrCurrHealth = JsonMapper.ToJson(200);//using LitJson;
            string saveJsonStrPosition = JsonMapper.ToJson(data);//using LitJson;

            //����StreamWriterд������ע����Ҫһ��string��������·����
            StreamWriter streamWriter = new StreamWriter(filePath);

            //��ת������ַ���д��Ŀ���ļ�
            //streamWriter.Write(saveJsonStrCurrHealth);
            streamWriter.Write(saveJsonStrPosition);


            //�ر�StreamWriter
            streamWriter.Close();
        }
    }

    public void LoadByJson()
    {
        //string����·�����������룩
        string directoryPath = Application.dataPath + "/StreamingFile";
        string filePath = directoryPath + "/byJson.json";

        if (!File.Exists(filePath))
        {
            SaveByJson();
        }
        else
        {
            //�����ļ���ȡ����ע��new����ʱ��Ҫstring���Ͳ�������·����
            StreamReader streamReader = new StreamReader(filePath);

            //��ȡ�ļ������е��ַ�����ReadToEnd�����ȡ����󣬼���ȡ���У�
            string jsonStr = streamReader.ReadToEnd();

            //����ȡ�����ַ���ͨ��JsonMapper.ToObjectת��ΪSave���͵Ķ���JsonMapper.ToObject<����>(·��);��
            Data data = JsonMapper.ToObject<Data>(jsonStr);//using LitJson;

            //������Ϸ״̬��������Ϸ��Ҫ���б�д
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
        // �����˻�
        if (data.accountFile != null)
        {
            GameManager.Instance.LoadAccountFile(data.accountFile);
        }


        // �������
        if (data.playerFile != null)
        {
            PlayerController.Instance.GetComponent<PlayerFileManager>().LoadPlayerFile(data.playerFile);
        }

        // ���ù���
        if (data.enemyFiles != null)
        {
            EnemyManager.Instance.LoadEnemyFiles(data.enemyFiles);
        }
    }
}
