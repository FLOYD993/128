using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public static PlayerQuest instance;

    public int exp;

    public List<Quest> questList = new List<Quest>(); //��ҳ��е����񼯺�
    private void Awake()
    {
        instance = this;
    }
}
