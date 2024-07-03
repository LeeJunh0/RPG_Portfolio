using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest_Item : UIBase
{
    enum GameObjects
    {
        QuestIcon,
        QuestNameText
    }

    string myName;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.QuestNameText).GetComponent<Text>().text = myName;
        Get<GameObject>((int)GameObjects.QuestIcon).BindEvent((PointerEventData) =>
        {

        });
    }

    public void SetInfo(string name)
    {
        myName = name;
    }
}
