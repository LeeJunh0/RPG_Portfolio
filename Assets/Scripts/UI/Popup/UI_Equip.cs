using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equip : UIPopup
{
    enum GameObjects
    {
        UI_Equip_Sword,
        UI_Equip_Armor,
        UI_Equip_Pant,
        UI_Equip_Boots,
        UI_Equip_ExitButton
    }

    enum Texts
    {
        UI_Equip_Hp_Text,
        UI_Equip_Mp_Text,
        UI_Equip_Att_Text,
        UI_Equip_Def_Text,
        UI_Equip_Move_Text
    }

    public override void Init()
    {
        base.Init();
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.UI_Equip_ExitButton).BindEvent(evt => { Managers.UI.ClosePopupUI(); });
        SetSlots();

        Managers.Equip.OnStatusSet -= StatusSet;
        Managers.Equip.OnStatusSet += StatusSet;
        StatusSet();
    }

    public void SetSlots()
    {
        for (int i = 0; i < Managers.Equip.slots.Length; i++)
        {
            GameObject go = GetObject((int)GameObjects.UI_Equip_Sword + i);
            UI_EquipSlot slot = go.GetComponent<UI_EquipSlot>();
            Managers.Equip.slots[i] = slot;
        }
    }

    public void StatusSet()
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        Text bindCheck = GetText((int)Texts.UI_Equip_Hp_Text);
        if (bindCheck == null)
            return;

        GetText((int)Texts.UI_Equip_Hp_Text).text = string.Format($"{playerStat.Hp} / {playerStat.MaxHp}");
        GetText((int)Texts.UI_Equip_Mp_Text).text = string.Format($"{playerStat.Mp} / {playerStat.MaxMp}");
        GetText((int)Texts.UI_Equip_Att_Text).text = string.Format($"{playerStat.Attack}");
        GetText((int)Texts.UI_Equip_Def_Text).text = string.Format($"{playerStat.Defense}");
        GetText((int)Texts.UI_Equip_Move_Text).text = string.Format($"{playerStat.Movespeed}");
    }
}
