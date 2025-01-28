using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UIBase
{
    public override void Init()
    {
        Managers.UI.ShowSceneUI<UI_Stat>();
        Managers.UI.ShowSceneUI<UI_MiniMap>();

        Managers.Input.KeyAction -= Managers.Inventory.OnInventory;
        Managers.Input.KeyAction += Managers.Inventory.OnInventory; 

        Managers.Input.KeyAction -= Managers.Quest.OnQuest;
        Managers.Input.KeyAction += Managers.Quest.OnQuest; 

        Managers.Input.KeyAction -= Managers.Equip.OnEquip;
        Managers.Input.KeyAction += Managers.Equip.OnEquip;

        Managers.Input.KeyAction -= OnSkill;
        Managers.Input.KeyAction += OnSkill;

        Managers.Input.KeyAction -= OnPopup;
        Managers.Input.KeyAction += OnPopup;
    }

    public void OnSkill()
    {
        if (Input.GetKeyDown(BindKey.Skill))
            Managers.UI.OnGameUIPopup<UI_Skill>();
    }

    public void OnPopup()
    {
        if (Input.GetKeyDown(BindKey.Pause) == false)
            return;

        if (Managers.UI.popupStack.Count <= 0)
            Managers.UI.OnGameUIPopup<UI_Pause>();        
        else    
            Managers.UI.ClosePopupUI();      
    }
}
