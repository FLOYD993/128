using UnityEngine;
[System.Serializable] //表示可序列化，显示在Inspector窗口
public class Quest
{
    public enum QuestType { Gathering, Talking, Reach }; //收集/击杀、对话、探索到达某个区域
    public enum QuestStatus { Waiting, Accepted, Completed }; //等待、领取、完成

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;

    public int expRewards; //经验值
    
}
