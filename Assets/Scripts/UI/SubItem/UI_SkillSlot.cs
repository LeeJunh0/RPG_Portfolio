using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_SkillSlot : UI_Slot
{
    public static UI_SkillSlot instance;
    public SkillInfo skillInfo;
    RectTransform   rect;

    public override void Init()
    {
        instance = this;
        rect = GetComponent<RectTransform>();
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    public void SetInfo(SkillInfo info)
    {
        base.Init();

        skillInfo = info;

        Texture2D texture = Managers.Resource.Load<Texture2D>(skillInfo.icon);
        Image slotIcon = GetImage((int)Images.SlotIcon);
        slotIcon.sprite = Managers.UI.TextureToSprite(texture);
    }

    private void EnterSlotEvent(PointerEventData eventData)
    {
        UI_SkillTip.Instance.SetColor(1f);
        UI_SkillTip.Instance.SetToolTip(skillInfo);

        RectTransform tooltipRect = UI_SkillTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_SkillTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_SkillTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(tooltipRect.anchoredPosition.x, y);

        Cursor.visible = false;
    }

    private void ExitSlotEvent(PointerEventData eventData)
    {
        UI_SkillTip.Instance.SetColor(0f);
        Cursor.visible = true;
    }
}
