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
            Managers.UI.OnGameUIPopup<UI_Inven>();
        else if (key == BindKey.Quest)
            Managers.UI.OnGameUIPopup<UI_Quest>();
        else if (key == BindKey.Skill)
            Managers.UI.OnGameUIPopup<UI_Skill>();
        else if (key == BindKey.Pause)
            Managers.UI.OnGameUIPopup<UI_Pause>();
        else
            return;
    }
}
