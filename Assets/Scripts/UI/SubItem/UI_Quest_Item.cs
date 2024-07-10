using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest_Item : UIPopup
{
    enum QuestTitles
    {
        QuestNameText
    }

    enum QuestIcons
    {
        QuestIcon
    }

    string myName;
    int myId;
    public override void Init()
    {
        Bind<Text>(typeof(QuestTitles));
        Bind<Button>(typeof(QuestIcons));
        
        GetText((int)QuestTitles.QuestNameText).GetComponent<Text>().text = myName;
        GetButton((int)QuestIcons.QuestIcon).gameObject.BindEvent((PointerEventData) =>
        {
            UI_Quest quest = FindObjectOfType<UI_Quest>();
            quest.OnQuestPopup(myId);
        });
    }

    public void SetInfo(string name, int id)
    {
        myName = name;
        myId = id;
    }
}
