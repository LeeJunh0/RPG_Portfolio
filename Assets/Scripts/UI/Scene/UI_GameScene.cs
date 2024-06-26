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
            OnInventory();
        else if (key == BindKey.Quest)
            OnQuest();
        else if (key == BindKey.Skill)
            OnSkill();
        else if (key == BindKey.Pause)
            OnPause();
        else
            return;
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
        Debug.Log("OnQuest");
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
