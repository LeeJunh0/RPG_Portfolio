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
     * ���Կ� ���� �ִ� �޼���� Add, Delete, Swap ���� �ϱ�.
     * Add�� �ܺο��� �޾ƿ��°����� Delete�� ������ �����ϸ鼭 �����.
     * ���� �޼������ ���ӿ� �������Ѽ� ������Ѻ���.
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
            Debug.Log("�κ��丮�� ������ �����մϴ�.");
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
