using System;
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
    int questId = int.MaxValue;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Debug.Log("UI_Quest Binding");

        base.Init();
               
        if (list == null)
        {
            list = GetObject((int)GameObjects.QuestList);
            popup = GetObject((int)GameObjects.UI_Quest_Popup);
        }
            
        QuestListInit();
        popup.SetActive(false);
    }

    void QuestListInit()
    {
        foreach(Transform child in list.transform)
            Managers.Resource.Destroy(child.gameObject);
        
        for(int i = 0; i < Managers.Data.QuestDict.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Quest_Item>(parent : list.transform).gameObject;
            UI_Quest_Item questItem = item.GetOrAddComponent<UI_Quest_Item>();
            Debug.Log($"SetInfo할 아이콘 : {item.name}");
            questItem.SetInfo(Managers.Data.QuestDict[i].name, Managers.Data.QuestDict[i].id);
        }
    }

    public void OnQuestPopup(int id)
    {            
        popup.GetOrAddComponent<UI_Quest_Popup>().QuestPopupInit(id);
        if(id != questId)
        {
            questId = id;
            return;
        }

        questId = int.MaxValue;
        popup.SetActive(false);
    }
}
