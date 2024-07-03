using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest : UIPopup
{
    enum GameObjects
    {
        QuestList,
        UI_Quest_Popup
    }

    GameObject list;
    GameObject popup;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        list = GetObject((int)GameObjects.QuestList);
        popup = GetObject((int)GameObjects.UI_Quest_Popup);

        foreach (Transform child in list.transform)
            Managers.Resource.Destroy(child.gameObject);   
    }

    void QuestListInit()
    {
        for(int i = 0; i < Managers.Data.QuestDict.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Quest_Item>(parent : list.transform).gameObject;
            UI_Quest_Item questItem = item.GetOrAddComponent<UI_Quest_Item>();
            questItem.SetInfo(Managers.Data.QuestDict[i].name);
        }
    }

    public void OnQuestPopup()
    {
        if(popup.IsValid() == true)
        {
            popup.SetActive(false);
        }
        else
        {

        }
    }
}
