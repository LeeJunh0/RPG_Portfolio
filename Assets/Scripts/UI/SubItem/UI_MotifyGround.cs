using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_MotifyGround : MonoBehaviour
{
    public UI_MotifySlot[] slots;

    public void SetSlots()
    {
        slots = new UI_MotifySlot[transform.childCount];

        for (int i = 0; i < slots.Length; i++)
            slots[i] = transform.GetChild(i).GetComponent<UI_MotifySlot>();
    }

    public void CheckSlots(UI_MotifySlot slot)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != slot || slots[i] == null)            
                slots[i].isClick = false;
            else    
                slots[i].isClick = true;

            slots[i].SetColor();
        }
    }

    public void DeSelect()
    {
        if (UI_SkillTip.Instance == null)
            return;

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].isClick = false;
            slots[i].SetColor();
        }        
    }
}
