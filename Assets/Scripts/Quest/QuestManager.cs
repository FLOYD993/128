using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public Text[] questUIArray;
    private Color changeColor;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        changeColor = Color.white;
    }
    public void UpdateQuestList()//�ӵ����������������ɺ���Ҫ��������UI����ϵ������б�
    {
        for(int i = 0; i < PlayerQuest.instance.questList.Count; i++)
        {
            if (PlayerQuest.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                questUIArray[i].text = PlayerQuest.instance.questList[i].questName;
                questUIArray[i].color = changeColor;
            }
            else if(PlayerQuest.instance.questList[i].questStatus == Quest.QuestStatus.Completed)
            {
                changeColor = Color.red;
                questUIArray[i].text = PlayerQuest.instance.questList[i].questName;
                questUIArray[i].color = changeColor;
            }
            
        }
    }
}
