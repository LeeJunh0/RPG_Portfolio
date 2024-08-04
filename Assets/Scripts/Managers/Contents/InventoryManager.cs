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
    UI_Inven InvenObject;

    public void Init()
    {
        invenInfos = new Iteminfo[Managers.Option.InventoryCount];
    }

    public void AddItem(Iteminfo item)
    {
        if (invenInfos == null) 
            return;

        for(int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i] != Managers.Data.ItemDict[199])
                continue;

            int index = -1;
            if (item.uiInfo.isStack == true)
            {
                while (invenInfos[++index].id != item.id) ;
                invenInfos[index] = item;
                continue;
            }

            invenInfos[i] = item;
        }
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
        /*invenInfos = */invenInfos.OrderBy(item => item.id).ToArray();
    }

    public void SetInvenReference()
    {

    }

    public void InterLocking()
    {

    }
}
