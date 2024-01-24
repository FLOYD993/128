using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public static PlayerQuest instance;

    public int exp;

    public List<Quest> questList = new List<Quest>(); //玩家持有的任务集合
    private void Awake()
    {
        instance = this;
    }
}
