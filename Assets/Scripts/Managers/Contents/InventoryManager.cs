using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager
{
    private UsingItem[] invenInfos;
    private UI_Inven_Slot[] invenIcons;

    public int SelectIndex { get; set; }
    public Item[] InvenInfos { get { return invenInfos; } private set { } }
    public UI_Inven_Slot[] InvenIcons { get { return invenIcons; } private set { } }

    public class ItemComparer : IComparer<UsingItem>
    {
        public int Compare(UsingItem x, UsingItem y)
        {
            if (x.ItemInfo == null) 
                return 1;
            else if (y.ItemInfo == null) 
                return -1;
            else if (x.ItemInfo == null || y.ItemInfo == null)
                return 0;
            else 
                return x.ItemInfo.id.CompareTo(y.ItemInfo.id);
        }      
    }

    ItemComparer comparer = new ItemComparer();

    public void Init()
    {
        invenInfos = new UsingItem[Managers.Option.InventoryCount];

        for (int i = 0; i < invenInfos.Length; i++)
            invenInfos[i] = new UsingItem();        
    }

    public void OnInventory()
    {
        if (Input.GetKeyDown(BindKey.Inventory))
            Managers.UI.OnGameUIPopup<UI_Inven>();
    }

    public void UpdateSlotInfo(int index)
    {
        if (Util.FindChild<UI_Inven>(Managers.UI.Root) == false)
            return;

        invenIcons[index].SetInfo(invenInfos[index].ItemInfo);       
    }

    public void UpdateAllSlot()
    {
        for (int i = 0; i < invenInfos.Length; i++)
            UpdateSlotInfo(i);
    }

    public void AddItem(Iteminfo item)
    {
        if (invenInfos.Length <= 0 || item == null)
            return;
        
        if (item.isStack == true)
        {
            for (int i = 0; i < invenInfos.Length; i++)
            {
                if (invenInfos[i].ItemInfo == null)
                    continue;

                if (invenInfos[i].ItemInfo.id == item.id)
                {
                    invenInfos[i].ItemInfo.curStack++; 
                    UpdateSlotInfo(i);
                    Managers.Quest.OnGetQuestAction?.Invoke(item.GetItemName(), GetItemCount(item.GetItemName()));
                    return;
                }
            }
        }

        for (int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i].ItemInfo != null)
                continue;

            invenInfos[i].SetItem(item);
            UpdateSlotInfo(i);
            Managers.Quest.OnGetQuestAction?.Invoke(item.uiInfo.name, GetItemCount(item.uiInfo.name));
            break;
        }
    }

    public void RemoveItem(int index)
    {
        if (invenInfos.Length <= 0)
            return;

        invenInfos[index].SetItem(null);
        UpdateSlotInfo(index);
    }

    public bool RemoveItem(Iteminfo item)
    {
        for (int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i].ItemInfo == null)
                continue;

            if (invenInfos[i].ItemInfo.Equals(item) == true)
            {
                if (invenInfos[i].ItemInfo.isStack == true)
                {
                    invenInfos[i].ItemInfo.curStack -= 1;
                    UpdateSlotInfo(i);

                    if (invenInfos[i].ItemInfo.curStack <= 0)
                    {
                        invenInfos[i].SetItem(null);
                        UpdateSlotInfo(i);
                        TrimAll();
                    }
                }
                else
                {
                    invenInfos[i].SetItem(null);
                    UpdateSlotInfo(i);
                    TrimAll();                
                }

                return true;
            }
        }

        return false;
    }

    public void ChangeItem(int item1, int item2)
    {
        Iteminfo item = invenInfos[item1].ItemInfo;
        invenInfos[item1].SetItem(invenInfos[item2].ItemInfo);
        invenInfos[item2].SetItem(item);
    }

    public void TrimAll()
    {
        if (invenInfos.Length <= 0)
            return;

        int voidSearch = -1;
        while (++voidSearch < invenInfos.Length && invenInfos[voidSearch].ItemInfo != null)
        {
            if (voidSearch >= invenInfos.Length)
            {
                Debug.Log("빈칸이 존재하지 않아 공백제거 불가.");
                return;
            }
        }

        int itemSearch = voidSearch;

        while (true)
        {
            while (++itemSearch < invenInfos.Length && invenInfos[itemSearch].ItemInfo == null) ;

            if (itemSearch >= invenInfos.Length)
                break;

            invenInfos[voidSearch].SetItem(invenInfos[itemSearch].ItemInfo);
            invenInfos[itemSearch].SetItem(null);
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

    public void SetInvenReference(UI_Inven_Slot[] icons) { invenIcons = icons; }

    public void InterLocking()
    {
        if (invenIcons == null)
            return;

        UpdateAllSlot();  
    }

    public int GetItemCount(string target)
    {
        if (target == "")
            return -1;

        int curCount = 0;
        for(int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i].ItemInfo == null)
                continue;
            else
            {
                curCount = invenInfos[i].ItemInfo.uiInfo.name == target ? curCount + invenInfos[i].ItemInfo.curStack : curCount;
                break;
            }
        }                  
        return curCount;
    }

    public void InfoLink(int item1, int item2)
    {
        ChangeItem(item1, item2);
        UpdateSlotInfo(item1);
        UpdateSlotInfo(item2);
    }
}
