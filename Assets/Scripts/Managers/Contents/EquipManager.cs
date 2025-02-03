using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager
{
    public Action OnStatusSet = null;

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

        if (Util.FindChild<UI_Equip>(Managers.UI.Root) == null)
            return;
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (equipInfos[i] == null)
                    slots[i].SetBefore(emptyImage[i]);
                else
                    slots[i].SetInfo(equipInfos[i]);

                slots[i].index = i;
            }
        }  
    }

    public void ItemEquip(Iteminfo item)
    {
        switch (item.type)
        {
            case Define.ItemType.Weapon:
                equipInfos[0] = item;
                break;
            case Define.ItemType.Chest:
                equipInfos[1] = item; 
                break;
            case Define.ItemType.Pants:
                equipInfos[2] = item; 
                break;
            case Define.ItemType.Boots:
                equipInfos[3] = item; 
                break;
        }

        if(Util.FindChild(Managers.UI.Root) != null)
        {
            for (int i = 0; i < slots.Length; i++) 
            {
                if (equipInfos[i] == null || slots[i] == null)
                    continue;

                slots[i].SetInfo(equipInfos[i]);
                slots[i].index = i;
            }
        }

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.MaxHp += item.hp;
        playerStat.Attack += item.att;

        OnStatusSet?.Invoke();
    }

    public void ItemStatPlus()
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        for (int i = 0; i < equipInfos.Length; i++)
        {
            if (equipInfos[i] == null)
                continue;

            playerStat.MaxHp += equipInfos[i].hp;
            playerStat.Attack += equipInfos[i].att;
        }
    }

    public void ItemUnEquip(int index)
    {
        if (equipInfos[index] == null)
            return;

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.MaxHp -= equipInfos[index].hp;
        playerStat.Attack -= equipInfos[index].att;

        Managers.Inventory.AddItem(equipInfos[index]);
        equipInfos[index] = null;
        slots[index].SetBefore(emptyImage[index]);

        OnStatusSet?.Invoke();
    }
}
