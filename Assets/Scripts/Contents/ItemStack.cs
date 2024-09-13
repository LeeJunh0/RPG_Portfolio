using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public bool isStack { get { return useStack; } set { useStack = value; } }
    public int MyStack 
    { 
        get 
        {
            return myStack;
        } 
        set 
        { 
            myStack = value;

            if (MyStack <= 0)
                Managers.Inventory.RemoveItem(Managers.Inventory.SelectIndex);
        } 
    }

    bool useStack = false;
    int myStack = 1;

}
