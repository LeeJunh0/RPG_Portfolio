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

    public QuestInfo Quest { get; private set; }

    public override void Init()
    {
        Bind<Text>(typeof(GiverQuestTitle));
        Bind<Button>(typeof(GiverQuestIcon));

        GetButton((int)GiverQuestIcon.QuestIcon).gameObject.BindEvent((evt) =>
        {
            UI_Giver giver = FindObjectOfType<UI_Giver>();
            giver.OnGiverPopup(Quest);
        });
    }

    public void SetInfo(QuestInfo info)
    {
        Quest = info;

        GetText((int)GiverQuestTitle.QuestNameText).text = Quest.name;
    }
}
