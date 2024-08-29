using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Giver_Item : UIPopup
{
    enum GiverQuestTitle
    {
        QuestNameText
    }

    enum GiverQuestIcon
    {
        QuestIcon
    }

    public QuestInfo quest;

    public override void Init()
    { 
        Bind<Text>(typeof(GiverQuestTitle));
        Bind<GameObject>(typeof(GiverQuestIcon));
        GetObject((int)GiverQuestIcon.QuestIcon).BindEvent((evt) =>
        {
            UI_Giver giver = FindObjectOfType<UI_Giver>();
            giver?.OnGiverPopup(quest);
        });
    }

    public void SetInfo(QuestInfo info) 
    {
        quest = info;
        GetText((int)GiverQuestTitle.QuestNameText).text = quest.name;
    }
}
