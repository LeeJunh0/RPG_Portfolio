using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UI_Quest_Popup : UIPopup
{
    enum QuestPopupObject
    {
        QuestName,
        QuestDescript,
        QuestRewards,
        QuestCondition
    }

    public int QuestIndex { get; set; }

    public override void Init()
    {
        Bind<Text>(typeof(QuestPopupObject)); 
        Managers.Quest.OnCurrentUpdate -= ConditionUpdate;
        Managers.Quest.OnCurrentUpdate += ConditionUpdate;
    }

    public void QuestPopupInit(int index)
    {
        QuestIndex = index;
        gameObject.SetActive(true);
        Text rewards = GetText((int)QuestPopupObject.QuestRewards);
        GetText((int)QuestPopupObject.QuestName).text = string.Format($"{Managers.Quest.ActiveQuests[QuestIndex].QuestName}");
        GetText((int)QuestPopupObject.QuestDescript).text = string.Format($"{Managers.Quest.ActiveQuests[QuestIndex].Description}"); 
        GetText((int)QuestPopupObject.QuestCondition).text = string.Format($"현재 : {Managers.Quest.ActiveQuests[QuestIndex].Task.CurrentSuccess} / {Managers.Quest.ActiveQuests[QuestIndex].SuccessCount}");
        
        rewards.text = string.Format($"보상 : {Managers.Quest.ActiveQuests[QuestIndex].Rewards.experience} ");
        foreach (string item in Managers.Quest.ActiveQuests[QuestIndex].Rewards.items)
            rewards.text += string.Format($",{item} ");

        ConditionUpdate(QuestIndex);
    }

    void ConditionUpdate(int current)
    {       
        if (Managers.Quest.ActiveQuests.Count < QuestIndex)
            return;

        Text go = GetText((int)QuestPopupObject.QuestCondition);
        if (go == null)
            return;

        Quest quest = Managers.Quest.ActiveQuests[QuestIndex];
        string check = quest.IsComplete ? "O" : "X";      
        go.text = string.Format($"현재 : {quest.Task.CurrentSuccess} / {quest.SuccessCount}");
    }
}
