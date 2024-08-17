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

    public string MyName { get; private set; }
    public int MyIndex { get; private set; }

    public override void Init()
    {
        Bind<Text>(typeof(QuestTitles));
        Bind<Button>(typeof(QuestIcons));

        GetButton((int)QuestIcons.QuestIcon).gameObject.BindEvent((PointerEventData) =>
        {
            UI_Quest quest = FindObjectOfType<UI_Quest>();
            quest?.OnQuestPopup(MyIndex);
        });
    }

    public void SetInfo(string name, int index)
    {
        Debug.Log($"SetInfo 중인 아이콘 : {this.name}");
        MyName = name;
        GetText((int)QuestTitles.QuestNameText).GetComponent<Text>().text = MyName;

        MyIndex = index;
    }
}
