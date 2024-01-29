using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyFile
{
    public string type;

    public int currHealth;

    public int[] position;
}

public class EnemyFileManager : MonoBehaviour
{
    

    public EnemyFile SaveEnemyFile()
    {
        EnemyFile enemyFile = new EnemyFile();
        enemyFile.position = new int[2];

        Parameter parameter = GetComponentInChildren<FSM>().parameter;
        enemyFile.type = parameter.enemyType;
        enemyFile.currHealth = parameter.currHealth;

        enemyFile.position[0] = (int)GetComponentInChildren<FSM>().transform.position.x;
        enemyFile.position[1] = (int)GetComponentInChildren<FSM>().transform.position.y;

        return enemyFile;
    }

    public void LoadEnemyFile(EnemyFile file)
    {
        Parameter parameter = GetComponentInChildren<FSM>().parameter;

        parameter.currHealth = file.currHealth;
    }
}
