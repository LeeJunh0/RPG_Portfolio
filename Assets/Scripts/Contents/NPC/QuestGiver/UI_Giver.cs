using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

public class UI_Giver : UIPopup
{
    enum GiverObject
    {
        PossibleList,
        GiverQuest_Popup,
        UI_Giver_ExitButton
    }

    List<QuestInfo> ownerQuests = new List<QuestInfo>();
    GameObject popup;
    GameObject list;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GiverObject));
        popup = GetObject((int)GiverObject.GiverQuest_Popup);
        list = GetObject((int)GiverObject.PossibleList);
        
        popup.SetActive(false);

        GetObject((int)GiverObject.UI_Giver_ExitButton).BindEvent((evt) =>
        {
            Managers.UI.ClosePopupUI();
        });
    }

    public void ListUp()
    {
        if (ownerQuests == null) 
            return;

        for(int i = 0; i < ownerQuests.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Giver_Item>(parent: list.transform).gameObject;
            UI_Giver_Item giverItem = item.GetOrAddComponent<UI_Giver_Item>();
            giverItem.SetInfo(ownerQuests[i]);
            item.transform.localScale = Vector3.one;
        }
    }

    public void UIListInit(List<QuestInfo> quests) 
    { 
        ownerQuests = quests;
        ListUp();
    }

    public void OnGiverPopup(QuestInfo quest) 
    {
        popup.GetOrAddComponent<GiverQuest_Popup>().SetInfo(quest);
        popup.GetOrAddComponent<GiverQuest_Popup>().GiverPopupInit();
    }
}
