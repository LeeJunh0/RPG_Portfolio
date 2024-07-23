using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager
{
    List<Data.Iteminfo> itemInfos;
    List<UI_Inven_Item> itemList = new List<UI_Inven_Item>();

    public Action ItemUpdate;

    //TODO
    /*
     * 슬롯에 영향 주는 메서드들 Add, Delete, Swap 정리 하기.
     * Add야 외부에서 받아오는거지만 Delete는 과정을 생각하면서 만들것.
     * 이후 메서드들을 게임에 연동시켜서 실행시켜보기.
     */
    public void OnUpdate()
    {
        ItemUpdate.Invoke();
    }

    public void AddItem(Data.Iteminfo item)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i].MyInfo = item;
                return;
            }
            Debug.Log("인벤토리에 공간이 부족합니다.");
        }
    }

    public void DeleteItem()
    {
        
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
        itemInfos = new List<Data.Iteminfo>(itemList.Count);

        for(int i = 0; i < itemList.Count; i++)        
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

        Debug.Log(item1.uiInfo.name);
    }

    public void Clear()
    {
        if(itemInfos != null)
            itemInfos.Clear();
    }
}
