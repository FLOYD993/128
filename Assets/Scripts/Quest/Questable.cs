using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

//�ýű�������κο���ί�������NPC��Ϸ����
public class Questable : MonoBehaviour
{
    public Quest quest;

    
    public void DelegateQuest() //ί�����񣬽����ڿ�ί�������NPC�Ի���ɺ����
    {
        if(quest.questStatus == Quest.QuestStatus.Waiting)
        {
            //���ｫ�ᱻί��һ������
            PlayerQuest.instance.questList.Add(quest);
            quest.questStatus = Quest.QuestStatus.Accepted;
        }
        else
        {
            //�����Ѿ���������񣬲������ظ���ȡ
            Debug.Log(string.Format("Quest:{0} has accepted already",quest.questName));
        }
    }
    
}
