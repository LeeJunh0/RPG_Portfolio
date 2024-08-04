using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager
{
    //Todo
    //아이템에 우선순위를 만들어 가중치를 적용한다 Dict = 우선순위, 가중치
    Iteminfo[] invenInfos;
    UI_Inven_Item[] invenIcons;

    public int SelectIndex { get; set; }

    public void Init()
    {
        invenInfos = new Iteminfo[Managers.Option.InventoryCount];

        for (int i = 0; i < invenInfos.Length; i++)
            invenInfos[i] = null;
    }

    public void UpdateSlotInfo(int index)
    {
        invenIcons[index].SetInfo(invenInfos[index]);
    }

    public void UpdateAllSlot()
    {
        for(int i = 0; i < invenInfos.Length; i++)
            invenIcons[i].SetInfo(invenInfos[i]);
    }

    public void AddItem(Iteminfo item)
    {
        if (invenInfos == null) 
            return;

        int index = -1;

        if (invenInfos.Contains(item))
        {
            while (++index <= invenInfos.Length && invenInfos[index] != item) ;
            invenInfos[index].myStack++;
            return;
        }

        while (++index <= invenInfos.Length && invenInfos[index] != Managers.Data.ItemDict[199])
        {
            if (index >= invenInfos.Length)
                break;
        }            
        invenInfos[index] = item;
        UpdateSlotInfo(index);
    }

    public void RemoveItem(int index)
    {
        if (invenInfos == null)
            return;

        invenInfos[index] = Managers.Data.ItemDict[199];
    }

    public void TrimAll()
    {
        if (invenInfos == null)
            return;

        int voidSearch = -1;
        while (invenInfos[++voidSearch] == Managers.Data.ItemDict[199]) ;
        int itemSearch = voidSearch;

        while (true)
        {
            while (++itemSearch <= invenInfos.Length && invenInfos[itemSearch] != Managers.Data.ItemDict[199]) ;

            if (itemSearch <= invenInfos.Length)
                break;

            invenInfos[voidSearch] = invenInfos[itemSearch];
            invenInfos[itemSearch] = Managers.Data.ItemDict[199];
            voidSearch++;
        }
    }

    public void SortAll()
    {
        TrimAll();
        invenInfos = invenInfos.OrderBy(item => item.id).ToArray();
    }

    public void SetInvenReference(UI_Inven_Item[] icons)
    {
        invenIcons = icons;
    }

    public void InterLocking()
    {
        if (invenIcons == null)
            return;

        UpdateAllSlot();  
    }
}
