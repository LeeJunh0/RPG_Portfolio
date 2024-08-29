using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    List<QuestInfo> quests = new List<QuestInfo>();
    public IReadOnlyList<QuestInfo> Quests => quests;

    private void Awake()
    {
        Search();
    }

    public void Search()
    {
        for (int i = 0; i < Managers.Data.QuestDict.Count; i++)
            quests.Add(new QuestInfo(Managers.Data.QuestDict[i]));
    }

    public void RemoveQuest(QuestInfo quest)
    {
        quests.Remove(quest);
    }

    public void GiverUIOpen()
    {
        if (Util.FindChild<UI_Giver>(Managers.UI.Root, "UI_Giver") != null)        
            return;
        
        UI_Giver ui = Managers.UI.ShowPopupUI<UI_Giver>();

        ui.UIListInit(quests);
    }
}
