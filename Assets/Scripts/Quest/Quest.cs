using UnityEngine;
[System.Serializable] //��ʾ�����л�����ʾ��Inspector����
public class Quest
{
    public enum QuestType { Gathering, Talking, Reach }; //�ռ�/��ɱ���Ի���̽������ĳ������
    public enum QuestStatus { Waiting, Accepted, Completed }; //�ȴ�����ȡ�����

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;

    public int expRewards; //����ֵ
    
}
