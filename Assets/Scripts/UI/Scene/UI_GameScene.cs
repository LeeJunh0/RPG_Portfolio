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
    }
}
