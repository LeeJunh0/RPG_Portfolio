using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DropSlot : UI_Slot
{
    public override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        icon = GetComponent<Image>();
        gameObject.BindEvent(OnHighlight, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(OffHighlight, Define.EUiEvent.PointerExit);
        gameObject.BindEvent(OnDrop, Define.EUiEvent.Drop);              
    }

    void OnHighlight(PointerEventData eventData) { icon.color = Color.red; }
    void OffHighlight(PointerEventData eventData) { icon.color = Color.white; }
    void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject == this.gameObject)
            return;

        if (eventData.pointerDrag == null || eventData.pointerDrag.GetComponent<UI_DropSlot>() == true)
            return;

        int item1 = GetComponent<UI_Inven_Slot>().Index;
        int item2 = eventData.pointerDrag.transform.parent.GetComponent<UI_Inven_Slot>().Index;
        Managers.Inventory.InfoLink(item1, item2);
    }
}
