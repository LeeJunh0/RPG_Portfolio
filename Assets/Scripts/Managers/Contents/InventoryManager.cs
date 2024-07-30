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
    //�����ۿ� �켱������ ����� ����ġ�� �����Ѵ� Dict = �켱����, ����ġ

    List<UI_Inven_Item> itemList;
    GameObject Inven;

    public int CurItemIndex { get; set; }

    public void ListLoad()
    {
        Inven = GameObject.Find(typeof(UI_Inven).Name);
        itemList = new List<UI_Inven_Item>();

        GameObject content = Util.FindChild(parent: Inven.gameObject, "Content", true);

        foreach (Transform child in content.transform)
            itemList.Add(child.GetOrAddComponent<UI_Inven_Item>());
    }

    public void Interlocking()
    {
        if (itemList == null)
            ListLoad();

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].SetInfo(null);
            itemList[i].SetIndex(i);
        }
    }

    public void AddItem(Data.Iteminfo item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].GetInfo() == Managers.Data.ItemDict[199]) // 199�� ��ĭ
            {
                itemList[i].SetInfo(item);
                Debug.Log($"��ĭ�� Add�� ������ : {itemList[i].GetInfo().uiInfo.name}");
                return;
            }

            if (item.uiInfo.isStack == true)
            {
                for (int j = 0; j < itemList.Count; j++)
                {
                    if (itemList[j].GetInfo().id != item.id)
                        continue;

                    itemList[j].SetStack(itemList[i].GetStack() + 1);
                    return;
                }
            }

            continue;
        }
    }

    public void DeleteItem()
    {
        if (itemList[CurItemIndex].GetInfo() == Managers.Data.ItemDict[199] || CurItemIndex >= itemList.Capacity)
        {
            Debug.LogError("��ĭ or OutRange !!");
            return;
        }

        Debug.Log($"������ ������ : {itemList[CurItemIndex].GetInfo().uiInfo.name}");
        itemList[CurItemIndex].SetInfo(null);
        CurItemIndex = int.MaxValue;
    }

    public void SwapItem(UI_Inven_Item item1, UI_Inven_Item item2)
    {
        Data.Iteminfo temp = item1.GetInfo();
        item1.SetInfo(item2.GetInfo());
        item2.SetInfo(temp);
    }

    public void TrimAll()
    {
        //Todo
        //���� �˾ƺ��� ���� ¥�� ���� �˰��� ������ ã�� �����غ��°� ������.

        int voidSearch = -1;
        while (itemList[++voidSearch].GetInfo() != Managers.Data.ItemDict[199]) ;
        int itemSearch = voidSearch;

        
    }

    public void SortAll()
    {
        TrimAll();
        itemList.Sort((item1, item2) => item1.GetInfo().id.CompareTo(item2.GetInfo().id));
    }
}
