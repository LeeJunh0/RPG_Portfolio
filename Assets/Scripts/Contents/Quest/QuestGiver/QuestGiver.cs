using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    // 얘는 매니저가 아님. PlayerController와 같은것.
    // 자신만을 위한 기능들이 좀 있어야할 필요가있음.

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

    public void RewardsGive(QuestInfo quest)
    {
        for(int i = 0; i < quest.rewards.items.Count; i++)
        {
            //quest
        }        
    }

    public void GiverUIOpen()
    {
        UI_Giver ui = Managers.UI.ShowPopupUI<UI_Giver>();
        ui.UIListInit(quests);
    }
}
