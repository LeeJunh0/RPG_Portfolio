using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DragSlot : UI_Slot
{
    private void Start()
    {
        icon = GetComponent<Image>();
    }

    protected override void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !UI_SetDragSlot.instance.isDraging)
            return;

        icon.raycastTarget = false;
        UI_SetDragSlot.instance.hoverSlots.Add(icon);
    }

    protected override void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || UI_SetDragSlot.instance.isDraging)
            return;
        
        icon.raycastTarget = true;
        UI_SetDragSlot.instance.hoverSlots.Remove(icon);
    }

    protected override void OnBeginDrag(PointerEventData eventData)
    {
        UI_SetDragSlot.instance.dragSlot = this;
        UI_SetDragSlot.instance.DragSetIcon(icon);
        UI_SetDragSlot.instance.transform.position = eventData.position;
        UI_SetDragSlot.instance.isDraging = true;
        UI_SetDragSlot.instance.icon.raycastTarget = false;
    }

    protected override void OnDrag(PointerEventData eventData)
    {
        UI_SetDragSlot.instance.SetColor(0.6f);
        UI_SetDragSlot.instance.transform.position = eventData.position;
        Debug.Log(UI_SetDragSlot.instance.isDraging);
    }

    protected override void OnEndDrag(PointerEventData eventData)
    {
        UI_SetDragSlot.instance.SetColor(0);
        UI_SetDragSlot.instance.isDraging = false;
        UI_SetDragSlot.instance.icon.raycastTarget = true;
        UI_SetDragSlot.instance.gameObject.transform.position = new Vector3(99f, 99f, 99f);
        UI_SetDragSlot.instance.dragSlot = null;

        foreach (Image slot in UI_SetDragSlot.instance.hoverSlots)
            slot.raycastTarget = true;
    }

}
