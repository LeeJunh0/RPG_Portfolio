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

    public void CheckSlots(UI_MotifySlot slot)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != slot)            
                slots[i].isClick = false;
            else    
                slots[i].isClick = true;

            slots[i].SetColor();
        }

        switch (slot.motifyInfo.type)
        {
            case Define.EMotifyType.Initialize:
                UI_SkillTip.Instance.InitInfo = slot.motifyInfo;
                break;
            case Define.EMotifyType.Embodiment:
                UI_SkillTip.Instance.EmbodiInfo = slot.motifyInfo;
                break;
            case Define.EMotifyType.Movement:
                UI_SkillTip.Instance.MoveInfo = slot.motifyInfo;
                break;
        }
    }
}
