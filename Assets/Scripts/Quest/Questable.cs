using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

//该脚本添加在任何可以委派任务的NPC游戏身上
public class Questable : MonoBehaviour
{
    public Quest quest;

    
    public void DelegateQuest() //委派任务，将会在可委派任务的NPC对话完成后调用
    {
        if(quest.questStatus == Quest.QuestStatus.Waiting)
        {
            //人物将会被委派一个任务
            PlayerQuest.instance.questList.Add(quest);
            quest.questStatus = Quest.QuestStatus.Accepted;
        }
        else
        {
            //人物已经有这个任务，不必再重复领取
            Debug.Log(string.Format("Quest:{0} has accepted already",quest.questName));
        }
    }
    
}
