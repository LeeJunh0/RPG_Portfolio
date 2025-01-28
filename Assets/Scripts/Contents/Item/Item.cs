using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Iteminfo itemInfo;

    public Iteminfo ItemInfo => itemInfo;
    public void SetItem(Iteminfo info) 
    { 
        itemInfo = info;
        //itemInfo = info == null ? Managers.Data.ItemDict[199] : info;
    }

    public virtual void Use(int index) { }
}

