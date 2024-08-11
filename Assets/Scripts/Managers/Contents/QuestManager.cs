using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    //QuestManager는 진행중인 퀘스트를 저장하고 있는다
    //QuestManager는 퀘스트 상태 관리를 한다. 완료인지 진행중인지 포기하는지.
    //QuestManager는 퀘스트 
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
