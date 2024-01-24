using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public string questName;
    public enum QuestType { Gathering, Reach };
    public QuestType questType;

    [Header("是否完成寻找任务")]
    public bool hasFound;
    [Header("是否完成探索任务")]
    public bool hasReached;

    private void Update()
    {
        if(hasFound && Input.GetKeyDown(KeyCode.E))
        {
            QuestComplete();
            Destroy(gameObject);
        }
    }
    //这个方法会在任务完成之后调用
    public void QuestComplete()
    {
        for(int i = 0; i < PlayerQuest.instance.questList.Count; i++)
        {
            if(questName == PlayerQuest.instance.questList[i].questName && PlayerQuest.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                switch(questType)
                {
                    case QuestType.Gathering:
                        if(hasFound)
                        {
                            PlayerQuest.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.instance.UpdateQuestList();
                        }
                        break;
                    case QuestType.Reach:
                        if (hasReached)
                        {
                            PlayerQuest.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.instance.UpdateQuestList();
                        }
                        break;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(questType == QuestType.Gathering)
            {
                hasFound = true;
            }
            else if(questType == QuestType.Reach)
            {
                hasReached = true;
                QuestComplete();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (questType == QuestType.Gathering)
            {
                hasFound = false;
            }
        }
        }
}
