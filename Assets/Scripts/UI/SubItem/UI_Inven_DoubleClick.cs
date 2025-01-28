using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven_DoubleClick : UI_Slot
{
    private UI_Inven_Slot item;
    private bool isDoubleCheck = false;

    private void Start()
    {
        item = transform.parent.GetComponent<UI_Inven_Slot>(); 
        gameObject.BindEvent(DoubleClickEvent, Define.EUiEvent.Click);
    }

    void Update()
    {
        if(isDoubleCheck == true)
        {
            Managers.Inventory.InvenInfos[item.Index].Use(item.Index);
            isDoubleCheck = false;
        }
    }

    private void DoubleClickEvent(PointerEventData eventData)
    {
        if (item == null)
            return;

        if (eventData.clickCount >= 2)
            isDoubleCheck = true;
    }
}
