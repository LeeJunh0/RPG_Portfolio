using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSlot : UI_Slot
{
    public Skill skill;
    RectTransform rect;

    public override void Init()
    {
        base.Init();

        rect = GetComponent<RectTransform>();
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    void EnterSlotEvent(PointerEventData eventData)
    {
        UI_SkillTip.Instance.SetColor(1f);

        RectTransform tooltipRect = UI_SkillTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_SkillTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_SkillTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(tooltipRect.anchoredPosition.x, y);

        Cursor.visible = false;
    }

    void ExitSlotEvent(PointerEventData eventData)
    {
        UI_SkillTip.Instance.SetColor(0f);

        Cursor.visible = true;
    }
}
