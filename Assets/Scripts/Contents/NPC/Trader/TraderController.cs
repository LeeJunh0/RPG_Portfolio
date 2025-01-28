using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderController : NpcController
{
    public override void Init()
    {
        npcType = Define.ENpc.Trader;
        base.Init();
    }

    public void OnTraderUI()
    {
        if (Util.FindChild<UI_Trader>(Managers.UI.Root, "UI_Trader") != null)
            return;

        Managers.UI.ShowPopupUI<UI_Trader>();
        Managers.UI.OnGameUIPopup<UI_Inven>();
    }
}
