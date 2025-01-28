using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager
{
    public Texture2D[] emptyImage = new Texture2D[4];
    public Iteminfo[] equipInfos = new Iteminfo[4];
    public UI_EquipSlot[] slots = new UI_EquipSlot[4];

    public void Init()
    {
        emptyImage[0] = Managers.Resource.Load<Texture2D>("melee_background");
        emptyImage[1] = Managers.Resource.Load<Texture2D>("chest_background");
        emptyImage[2] = Managers.Resource.Load<Texture2D>("pants_background");
        emptyImage[3] = Managers.Resource.Load<Texture2D>("boots_background");
    }

    public void OnEquip()
    {
        if (Input.GetKeyDown(BindKey.Equipment))
            Managers.UI.OnGameUIPopup<UI_Equip>();
    }

    public void ItemEquip(Iteminfo item)
    {
        switch (item.type)
        {
            case Define.ItemType.Weapon:
                equipInfos[0] = item;
                slots[0].SetInfo(item);
                slots[0].index = 0;
                break;
            case Define.ItemType.Chest:
                equipInfos[1] = item; 
                slots[1].SetInfo(item);
                slots[1].index = 1;
                break;
            case Define.ItemType.Pants:
                equipInfos[2] = item; 
                slots[2].SetInfo(item);
                slots[2].index = 2;
                break;
            case Define.ItemType.Boots:
                equipInfos[3] = item; 
                slots[3].SetInfo(item);
                slots[3].index = 3;
                break;
        }

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.Hp += item.hp;
        playerStat.Attack += item.att;
    }

    //public void ItemUnEquip( item)
    //{
    //    PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
    //    playerStat.Hp -= equipInfos[index].hp;
    //    playerStat.Attack -= equipInfos[index].att;
    //
    //
    //}
}
