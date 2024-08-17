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
        list = GetObject((int)GameObjects.QuestList);
        popup = GetObject((int)GameObjects.UI_Quest_Popup);

        QuestListInit();
        popup.SetActive(false);
    }

    void QuestListInit()
    {
        // 실시간 Quest만큼 생성
        for (int i = list.transform.childCount; i < Managers.Quest.activeQuests.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Quest_Item>(parent: list.transform).gameObject;
            UI_Quest_Item questItem = item.GetOrAddComponent<UI_Quest_Item>();
            Debug.Log($"SetInfo할 아이콘 : {item.name}");
            questItem.SetInfo(Managers.Quest.activeQuests[i].QuestName, i);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    public void OnQuestPopup(int id)
    {
        popup.GetOrAddComponent<UI_Quest_Popup>().QuestPopupInit(id);
        if (id != questId)
        {
            questId = id;
            return;
        }

        questId = int.MaxValue;
        popup.SetActive(false);
    }

    private void Update()
    {
        QuestListInit();
    }
}
