using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DropSlot : UI_Slot
{
    private void Start()
    {
        icon = GetComponent<Image>();
    }

    protected override void OnPointerEnter(PointerEventData eventData) { icon.color = Color.red; }
    protected override void OnPointerExit(PointerEventData eventData) { icon.color = Color.white; }
    protected override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject == this.gameObject)
            return;

        if (eventData.pointerDrag == null || eventData.pointerDrag.GetComponent<UI_DropSlot>() == true)
            return;

        int item1 = GetComponent<UI_Inven_Slot>().Index;
        int item2 = eventData.pointerDrag.transform.parent.GetComponent<UI_Inven_Slot>().Index;
        Managers.Inventory.InfoLink(item1, item2);
    }

    //protected override void OnClick(PointerEventData eventData)
    //{
    //    Debug.Log($"æ∆¿Ãƒ‹ Index : {Index}");
    //    Managers.Inventory.SelectIndex = Index;
    //    UI_Inven inven = FindObjectOfType<UI_Inven>();
    //    inven?.OnInvenPopup(Index);
    //}
}
