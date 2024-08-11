using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UI_Quest_Popup : UIPopup
{
    enum QuestPopupObject
    {
        QuestName,
        QuestDescript,
        QuestCompleted,
        QuestRewards,
        QuestConditions
    }

    public override void Init()
    {
        Bind<Text>(typeof(QuestPopupObject));
    }

    public void QuestPopupInit(int id)
    {
        gameObject.SetActive(true);
        string check = Managers.Data.QuestDict[id].isCompleted ? "O" : "X";
        GetText((int)QuestPopupObject.QuestName).text = string.Format($"{Managers.Data.QuestDict[id].name}");
        GetText((int)QuestPopupObject.QuestDescript).text = string.Format($"{Managers.Data.QuestDict[id].description}");    
        GetText((int)QuestPopupObject.QuestCompleted).text = string.Format($"완료 : {check}");
        Text rewards = GetText((int)QuestPopupObject.QuestRewards);
        rewards.text = string.Format($"보상 : {Managers.Data.QuestDict[id].rewards.experience}, ");

        foreach (string item in Managers.Data.QuestDict[id].rewards.items)
            rewards.text += string.Format($"{item}, ");

        GameObject conditions = GetObject((int)QuestPopupObject.QuestConditions);
        for(int i = 0; i < Managers.Data.QuestDict[id].conditions.Count; i++)
        {

        }
    }
}
