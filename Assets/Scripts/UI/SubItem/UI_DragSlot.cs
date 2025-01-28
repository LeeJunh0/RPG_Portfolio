using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DragSlot : UI_Slot
{
    public override void Init()
    {
        base.Init();        
    }

    private void Start()
    {
        icon = GetComponent<Image>();
        gameObject.BindEvent(OffRayTarget, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(OnRayTarget,Define.EUiEvent.PointerExit);
        gameObject.BindEvent(StartDragging,Define.EUiEvent.BeginDrag);
        gameObject.BindEvent(Dragging,Define.EUiEvent.Drag);
        gameObject.BindEvent(EndDragging,Define.EUiEvent.EndDrag);      
    }

    private void OffRayTarget(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !UI_SetDragSlot.instance.isDraging)
            return;

        icon.raycastTarget = false;
        UI_SetDragSlot.instance.hoverSlots.Add(icon);
    }

    private void OnRayTarget(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || UI_SetDragSlot.instance.isDraging)
            return;
        
        icon.raycastTarget = true;
        UI_SetDragSlot.instance.hoverSlots.Remove(icon);
    }

    private void StartDragging(PointerEventData eventData)
    {
        UI_SetDragSlot.instance.dragSlot = this;
        UI_SetDragSlot.instance.DragSetIcon(icon);
        UI_SetDragSlot.instance.transform.position = eventData.position;
        UI_SetDragSlot.instance.isDraging = true;
        UI_SetDragSlot.instance.icon.raycastTarget = false;
    }

    private void Dragging(PointerEventData eventData)
    {
        UI_SetDragSlot.instance.SetColor(0.6f);
        UI_SetDragSlot.instance.transform.position = eventData.position;
    }

    private void EndDragging(PointerEventData eventData)
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
