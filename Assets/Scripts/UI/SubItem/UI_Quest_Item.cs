using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest_Item : UI_Quest
{
    enum ItemObjects
    {
        QuestIcon,
        QuestNameText
    }

    string myName;
    int myId;

    public override void Init()
    {
        //TODO
        // 초기화 다시생각
        Bind<GameObject>(typeof(ItemObjects));

        Get<GameObject>((int)ItemObjects.QuestNameText).GetComponent<Text>().text = myName;
        Get<GameObject>((int)ItemObjects.QuestIcon).BindEvent((PointerEventData) =>
        {
            if (popup != null)
                OnQuestPopup();
            else
                Debug.Log("popup null !!");
        });
    }

    public void SetInfo(string name, int id)
    {
        myName = name;
        myId = id;
    }
}
