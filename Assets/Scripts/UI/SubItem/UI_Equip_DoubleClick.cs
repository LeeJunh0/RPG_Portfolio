using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Equip_DoubleClick : MonoBehaviour
{
    private UI_EquipSlot slot;
    private bool isDoubleCheck = false;

    private void Start()
    {
        slot = transform.parent.GetComponent<UI_EquipSlot>();
        gameObject.BindEvent(DoubleClickEvent, Define.EUiEvent.Click);
    }

    void Update()
    {
        if (isDoubleCheck == true)
        {
            Managers.Equip.ItemUnEquip(slot.index);
            isDoubleCheck = false;
        }
    }

    private void DoubleClickEvent(PointerEventData eventData)
    {
        if (slot == null)
            return;

        if (eventData.clickCount >= 2)
            isDoubleCheck = true;
    }
}
