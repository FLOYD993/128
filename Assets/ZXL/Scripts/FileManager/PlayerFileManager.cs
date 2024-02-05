using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerFile
{
    public int currHealth;

    public int[] position;
}

public class PlayerFileManager : MonoBehaviour
{
    private PlayerFile playerFile = new PlayerFile();

    public PlayerFile SavePlayerFile()
    {
        PlayerFile playerFile = new PlayerFile();
        playerFile.position = new int[2];

        PlayerParameter parameter = GetComponentInChildren<PlayerSaveFileController>().parameter;
       
        playerFile.currHealth = parameter.currHealth;

        playerFile.position[0] = (int)transform.position.x;
        playerFile.position[1] = (int)transform.position.y;

        return playerFile;
    }

    public void LoadPlayerFile(PlayerFile file)
    {
        if (file == null) { return; }

        playerFile = file;

        PlayerParameter parameter = GetComponentInChildren<PlayerSaveFileController>().parameter;
        parameter.currHealth = playerFile.currHealth;

        transform.position = new Vector3(playerFile.position[0], playerFile.position[1], 0F);

    }
}
