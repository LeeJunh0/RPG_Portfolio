using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class QuestManager
{
    List<Quest> activeQuests = new List<Quest>();

    public Action<QuestInfo> OnStartQuest = null;
    public Action<Quest> OnCompletedQuest = null;
    public Action<Quest> OnRewardsQuest = null;

    public Action<string> OnKillQuestAction = null;
    public Action<string, int> OnGetQuestAction = null;
    public Action<int> OnLevelQuestAction = null;

    public Action<int> OnCurrentUpdate = null;

    public IReadOnlyList<Quest> ActiveQuests => activeQuests;

    public void Init()
    {
        OnStartQuest -= AddQuest;
        OnStartQuest += AddQuest;
        OnCompletedQuest -= CompleteQuest;
        OnCompletedQuest += CompleteQuest;

        OnKillQuestAction -= UpdateKill;
        OnKillQuestAction += UpdateKill;
        OnGetQuestAction -= UpdateGet;
        OnGetQuestAction += UpdateGet;
        OnLevelQuestAction -= UpdateLevel;
        OnLevelQuestAction += UpdateLevel;
    }

    public void OnQuest()
    {
        if (Input.GetKeyDown(BindKey.Quest))
            Managers.UI.OnGameUIPopup<UI_Quest>();
    }

    public void AddQuest(QuestInfo questInfo) 
    {     
        Quest quest = new Quest(questInfo);

        if(activeQuests.Count <= 0)
            activeQuests.Add(quest);
        else
        {
            bool check = true;
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].QuestName == quest.QuestName)
                {
                    check = false;
                    break;
                }    
            }

            if (check == true)
                activeQuests.Add(quest);
        }

        if (quest.Task.Type == (int)Define.EQuestEvent.Level)
            OnLevelQuestAction?.Invoke(Managers.Game.GetPlayer().GetComponent<PlayerStat>().Level);

        if (quest.Task.Type == (int)Define.EQuestEvent.Get)
            OnGetQuestAction?.Invoke(quest.Task.Target, Managers.Inventory.GetItemCount(quest.Task.Target));
    }

    public void RemoveQuest(Quest quest) 
    { 
        activeQuests.Remove(quest); 
    }

    public void CompleteQuest(Quest quest)
    {
        RemoveQuest(quest);

        foreach(string name in quest.Rewards.items)
        {
            for(int i = 0; i < Managers.Data.ItemDict.Count - 1; i++)
            {
                if (Managers.Data.ItemDict[102 + i].uiInfo.name == name)
                {
                    Managers.Inventory.AddItem(new Iteminfo(Managers.Data.ItemDict[102 + i]));
                }
            }
        }
            
    }

    public void UpdateKill(string target)
    {
        if (activeQuests.Count <= 0)
            return;

        for(int i = 0; i < ActiveQuests.Count; i++)
            ActiveQuests[i].Task.Counting(target);      
    }

    public void UpdateGet(string target, int count)
    {
        if (activeQuests.Count <= 0)
            return;

        for (int i = 0; i < ActiveQuests.Count; i++)
            ActiveQuests[i].Task.CountSet(target, count);
    }

    public void UpdateLevel(int curLevel)
    {
        if (activeQuests.Count <= 0)
            return;

        for (int i = 0; i < ActiveQuests.Count; i++)
            ActiveQuests[i].Task.CountSet("Level", curLevel);
    }
}
