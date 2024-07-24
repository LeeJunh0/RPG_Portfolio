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
    List<UI_Inven_Item> itemList = new List<UI_Inven_Item>();

    public GameObject CurItem { get; set; }

    public void AddItem(Data.Iteminfo item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].MyInfo.id == 101) // 101은 빈칸
            {
                itemList[i].MyInfo = item;
                return;
            }
            
            if(item.uiInfo.isStack == true)
            {
                for(int j = 0; j < itemList.Count; j++)
                {
                    if (itemList[j].MyInfo.id != item.id) 
                        continue;

                    itemList[j].MyStack++;
                    return;
                }
            }

            Debug.Log("인벤토리에 공간이 부족합니다.");
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
        CurItem.GetComponent<UI_Inven_Item>().MyInfo = Managers.Data.ItemDict[101];
        CurItem = null;
    }

    public void ListLoad()
    {
        GameObject content = Util.FindChild(parent: Managers.UI.Root, "Content", true);

        foreach (Transform icon in content.transform)       
            itemList.Add(icon.GetOrAddComponent<UI_Inven_Item>());        
    }

    public void Interlocking()
    {
        ListLoad();
        /*
         * 정보리스트가 빈상태면 빈칸으로 채우고 리턴
         * 아니라면 정보리스트에 있는걸 실제 UI에 전달.
        */
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
        itemList.Sort();
    }

    public void Clear()
    {
        if(itemInfos != null)
            itemInfos.Clear();
    }
}
