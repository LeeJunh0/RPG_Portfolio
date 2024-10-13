using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MotifyGround : MonoBehaviour
{
    [SerializeField]
    UI_MotifySlot[] slots;
   
    public void SetSlots()
    {
        slots = new UI_MotifySlot[transform.childCount];

        for (int i = 0; i < slots.Length; i++)
            slots[i] = transform.GetChild(i).GetComponent<UI_MotifySlot>();
    }

    public void CheckSlots()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isClick == true)
                slots[i].isClick = false;
        }
    }
}
