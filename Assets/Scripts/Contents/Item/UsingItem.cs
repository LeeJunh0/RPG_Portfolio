using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class UsingItem : Item
{
    public override void Use(int index)
    {
        if (itemInfo == null)
            return;

        if (ItemInfo.type == ItemType.None)
            return;

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        switch (ItemInfo.type)
        {
            case ItemType.None:
                break;
            case ItemType.Consumable:
                playerStat.OnHealing(ItemInfo.hp);
                Debug.Log("소모품 소모");
                break;
            default:
                Managers.Equip.ItemEquip(ItemInfo);
                break;
        }

        if (ItemInfo.isStack == true)
        {
            ItemInfo.curStack -= 1;
            
            if (itemInfo.curStack <= 0)
                Managers.Inventory.RemoveItem(index);
        }
        else
            Managers.Inventory.RemoveItem(index);

        Managers.Inventory.UpdateAllSlot();
    }
}
