using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance { get => instance; set => instance = value; }

    public GameObject[] childrenOG;
    // public EnemyFile[] enemyFiles;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        childrenOG =  GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var child in childrenOG)
        {
            // Debug.Log("!!!!!" + child.name);
        }
    }

    public EnemyFile[] SaveEnemyFiles()
    {
        EnemyFile[] enemyFiles = new EnemyFile[childrenOG.Length];

        for (int i = 0;i < enemyFiles.Length; i++)
        {
            enemyFiles[i] = childrenOG[i].GetComponent<EnemyFileManager>().SaveEnemyFile();

            Debug.Log(enemyFiles[i]);
        }



        return enemyFiles;
    }

    public void LoadEnemyFiles(EnemyFile[] files)
    {
        if(files == null) { return; }

        ClearEnemies();

        foreach (EnemyFile file in files)
        {
            GameObject enemyTemp = Instantiate(PrefabUtility.LoadPrefabContents(file.type),                 // 创建类型
                                                new Vector3(file.position[0], file.position[1], 0F),        // 创建位置
                                                Quaternion.identity,                                        // 旋转
                                                transform);                                                 // 父物体，EnemyManager

            enemyTemp.GetComponent<EnemyFileManager>().LoadEnemyFile(file);
        }


    }


    public void ClearEnemies()
    {
        foreach (var child in childrenOG)
        {
            Destroy(child.gameObject);
        }
    }
}
