using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    //QuestManager�� �������� ����Ʈ�� �����ϰ� �ִ´�
    //QuestManager�� ����Ʈ ���� ������ �Ѵ�. �Ϸ����� ���������� �����ϴ���.
    //QuestManager�� ����Ʈ 
    List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest) { activeQuests.Add(new Quest(quest)); }
    public void RemoveQuest(Quest quest) { activeQuests.Remove(quest); }
    public void CompleteQeust(Quest quest)
    {

    }

    public void UpdateQuest()
    {
        if (activeQuests.Count <= 0)
            return;

        foreach(Quest quest in activeQuests)
        {

        }
    }

    public void UpdateKill(string target)
    {
        foreach(Quest quest in activeQuests)
        {

        }
    }

    public void UpdateGet(string target)
    {

    }

    public void UpdateLevel(int target)
    {

    }
}
