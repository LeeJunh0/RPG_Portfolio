using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UIBase
{
    public override void Init()
    {
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
    }

    void OnKeyEvent(KeyCode key)
    {
        if (key == BindKey.Inventory)
            OnGameUIPopup<UI_Inven>();
        else if (key == BindKey.Quest)
            OnQuest();
        else if (key == BindKey.Skill)
            OnSkill();
        else if (key == BindKey.Pause)
            OnPause();
        else
            return;
    }

    void OnGameUIPopup<T>() where T : UIPopup
    {
        T uiType = Util.FindChild<T>(Managers.UI.Root);
        Debug.Log($"On{typeof(T)}");

        if (uiType != null)
            uiType.ClosePopupUI();
        else
            Managers.UI.ShowPopupUI<T>();
    }

    void OnInventory()
    {
        UI_Inven inven = Util.FindChild<UI_Inven>(Managers.UI.Root);
        Debug.Log("OnInventory");

        if (inven != null)
            inven.ClosePopupUI();
        else
            Managers.UI.ShowPopupUI<UI_Inven>();
    }
    
    void OnQuest()
    {
        UI_Quest quest = Util.FindChild<UI_Quest>(Managers.UI.Root);
        Debug.Log("OnQuest");

        if (quest != null)
            quest.ClosePopupUI();
        else
            Managers.UI.ShowPopupUI<UI_Quest>();
    }

    void OnSkill()
    {
        Debug.Log("OnSkill");
    }

    void OnPause()
    {
        Debug.Log("OnPause");
    }
}
