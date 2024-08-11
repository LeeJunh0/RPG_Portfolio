using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager
{
    Iteminfo[] invenInfos;
    UI_Inven_Item[] invenIcons;

    public int SelectIndex { get; set; }

    public class ItemComparer : IComparer<Iteminfo>
    {
        public int Compare(Iteminfo x, Iteminfo y)
        {
            if (x == null) 
                return 1;
            else if (y == null) 
                return -1;
            else if (x == null || y == null)
                return 0;
            else 
                return x.id.CompareTo(y.id);
        }      
    }

    ItemComparer comparer = new ItemComparer();

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
        for (int i = 0; i < invenInfos.Length; i++)
            UpdateSlotInfo(i);
    }

    public void AddItem(Iteminfo item)
    {
        if (invenInfos == null)
            return;

        if (item.uiInfo.isStack == true)
        {
            for (int i = 0; i < invenInfos.Length; i++)
            {
                if (invenInfos[i] == null)
                    continue;

                if (invenInfos[i].id == item.id)
                {
                    invenInfos[i].MyStack++;
                    UpdateSlotInfo(i);
                    return;
                }
            }
        }

        for (int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i] != null)
                continue;

            invenInfos[i] = item;
            UpdateSlotInfo(i);
            return;
        }
    }

    public void RemoveItem(int index)
    {
        if (invenInfos == null)
            return;

        invenInfos[index] = null;
        UpdateSlotInfo(index);
    }

    public void TrimAll()
    {
        if (invenInfos == null)
            return;

        int voidSearch = -1;
        while (++voidSearch < invenInfos.Length && invenInfos[voidSearch] != null)
        {
            if (voidSearch >= invenInfos.Length)
            {
                Debug.Log("��ĭ�� �������� �ʾ� �������� �Ұ�.");
                return;
            }
        }

        int itemSearch = voidSearch;

        while (true)
        {
            while (++itemSearch < invenInfos.Length && invenInfos[itemSearch] == null) ;

            if (itemSearch >= invenInfos.Length)
                break;

            invenInfos[voidSearch] = invenInfos[itemSearch];
            invenInfos[itemSearch] = null;
            voidSearch++;
        }
        UpdateAllSlot();
    }

    public void SortAll()
    {
        TrimAll();
        Array.Sort(invenInfos, comparer);
        UpdateAllSlot();
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
