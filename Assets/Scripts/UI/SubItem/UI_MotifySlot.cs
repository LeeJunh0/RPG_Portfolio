using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MotifySlot : UI_Slot
{
    public bool         isClick = false;
    public MotifyInfo   motifyInfo;
    RectTransform       rect;

    public override void Init()
    {       
        icon = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        gameObject.BindEvent(OnClickEvent, Define.EUiEvent.Click);
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    public void SetInfo(MotifyInfo info) 
    {
        base.Init();

        motifyInfo = info;

        Texture2D texture = Managers.Resource.Load<Texture2D>(motifyInfo.icon);
        Image slotIcon = GetImage((int)Images.SlotIcon);
        slotIcon.sprite = Managers.UI.TextureToSprite(texture);
    }

    public void SetColor()
    {
        icon.color = isClick == true ? Color.red : Color.white;
    }
    
    void OnClickEvent(PointerEventData eventData)
    {
        UI_MotifyGround parent = transform.parent.GetComponent<UI_MotifyGround>();
        parent.CheckSlots(this);

        Motify motify = Managers.Skill.SetMotify(motifyInfo.skillName);
        Managers.Skill.playerInventory.AddMotify(motifyInfo);
    }

    void EnterSlotEvent(PointerEventData eventData)
    {
        UI_MotifyTip.Instance.SetToolTip(motifyInfo);
        UI_MotifyTip.Instance.SetColor(1f);

        RectTransform tooltipRect = UI_MotifyTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_MotifyTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_MotifyTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(tooltipRect.anchoredPosition.x, y);

        Cursor.visible = false;
    }

    void ExitSlotEvent(PointerEventData eventData)
    {
        UI_MotifyTip.Instance.SetColor(0f);

        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
