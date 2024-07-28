using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager
{
    List<Data.Iteminfo> itemInfos;
    List<UI_Inven_Item> itemList;
    GameObject Inven;

    public UI_Inven_Item CurItem { get; set; }

    public void AddItem(Data.Iteminfo item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].MyInfo == Managers.Data.ItemDict[199]) // 101은 빈칸
            {
                itemList[i].MyInfo = item;
                itemInfos[i] = itemList[i].MyInfo;
                Debug.Log($"빈칸에 Add한 아이템 : {itemInfos[i].uiInfo.name}");
                return;
            }

            if (item.uiInfo.isStack == true)
            {
                for (int j = 0; j < itemList.Count; j++)
                {
                    if (itemList[j].MyInfo.id != item.id)
                        continue;

                    itemList[j].MyStack++;
                    return;
                }
            }

            continue;
        }
    }

    public void DeleteItem()
    {
        if (CurItem == null)
        {
            Debug.Log("삭제할 아이템이 정해지지 않았습니다 !!");
            return;
        }

        Debug.Log($"삭제된 아이템 : {CurItem.GetComponent<UI_Inven_Item>().MyInfo.uiInfo.name}");
        CurItem.MyInfo = null;
        CurItem = null;
    }

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
        
        if(itemInfos == null)
        {
            itemInfos = new List<Data.Iteminfo>(itemList.Count);
            
            for(int i = 0; i < itemList.Count; i++)
            {
                itemList[i].MyInfo = null;
                itemInfos.Add(itemList[i].MyInfo);
            }
            return;
        }

        ItemInfosLoad();
    }

    public void ItemInfosLoad()
    {
        for (int i = 0; i < itemList.Count; i++)
            itemList[i].MyInfo = itemInfos[i];
    }

    public Data.Iteminfo GetItem(int index)
    { 
        return itemInfos[index];
    }

    public void SwapItem(Data.Iteminfo item1, Data.Iteminfo item2)
    {
        Data.Iteminfo temp = item1;
        item2 = item1;
        item1 = temp;
        temp = null;
    }

    public void ItemSort()
    {
        //Todo
        //정렬 알고리즘에 조건 걸어서 빈칸은 전부 제외시키는것으로..
        //이후 정렬한 itemList에 있는 정보들을 매니저에서 저장하고 있도록.

        itemList.Sort((item1, item2) => item1.MyInfo.id.CompareTo(item2.MyInfo.id));
        ItemInfosLoad();
    }

    public void Clear()
    {
        if(itemInfos != null)
            itemInfos.Clear();
    }
}
