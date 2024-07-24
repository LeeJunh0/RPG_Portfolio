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
            if (itemList[i].MyInfo.id == 101) // 101�� ��ĭ
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

            Debug.Log("�κ��丮�� ������ �����մϴ�.");
        }
    }

    public void DeleteItem()
    {
        if (CurItem == null)
        {
            Debug.Log("������ �������� �������� �ʾҽ��ϴ� !!");
            return;
        }

        Debug.Log($"������ ������ : {CurItem.GetComponent<UI_Inven_Item>().MyInfo.uiInfo.name}");
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
         * ��������Ʈ�� ����¸� ��ĭ���� ä��� ����
         * �ƴ϶�� ��������Ʈ�� �ִ°� ���� UI�� ����.
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
