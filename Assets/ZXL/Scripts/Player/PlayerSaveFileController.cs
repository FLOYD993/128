using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveFileController : MonoBehaviour
{
    private static PlayerSaveFileController instance;

    public PlayerParameter parameter = new PlayerParameter();

    public static PlayerSaveFileController Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }
}
