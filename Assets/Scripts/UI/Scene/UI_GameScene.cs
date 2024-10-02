using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UIBase
{
    public override void Init()
    {
        Managers.UI.ShowPopupUI<UI_Stat>();
        Managers.UI.ShowPopupUI<UI_MiniMap>();

        Managers.Input.KeyAction -= Managers.Inventory.OnInventory;
        Managers.Input.KeyAction += Managers.Inventory.OnInventory; 
        Managers.Input.KeyAction -= Managers.Quest.OnQuest;
        Managers.Input.KeyAction += Managers.Quest.OnQuest;
        Managers.Input.KeyAction -= OnPause;
        Managers.Input.KeyAction += OnPause;
        Managers.Input.KeyAction -= OnSkill;
        Managers.Input.KeyAction += OnSkill;
    }

    public void OnPause()
    {
        if (Input.GetKeyDown(BindKey.Pause))
            Managers.UI.OnGameUIPopup<UI_Pause>();
    }

    public void OnSkill()
    {
        if (Input.GetKeyDown(BindKey.Skill))
            Managers.UI.OnGameUIPopup<UI_Skill>();
    }
}
