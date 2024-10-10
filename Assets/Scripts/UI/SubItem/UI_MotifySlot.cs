using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MotifySlot : UI_Slot
{
    public Motify motify;
    RectTransform rect;
    bool isClick = false;

    public override void Init()
    {
        base.Init(); 
        
        icon = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        gameObject.BindEvent(OnClickHighLight, Define.EUiEvent.Click);
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);

        Texture2D texture = Managers.Resource.Load<Texture2D>(motify.info.icon);
        GetImage((int)Images.SlotIcon).sprite = Managers.UI.TextureToSprite(texture);
    }

    void OnClickHighLight(PointerEventData eventData)
    {
        if(isClick == true)
        {
            isClick = false;
            icon.color = Color.white;
            return;
        }

        isClick = true;
        icon.color = Color.red;
    }

    void EnterSlotEvent(PointerEventData eventData)
    {
        UI_MotifylTip.Instance.SetToolTip(motify);
        UI_MotifylTip.Instance.SetColor(1f);

        RectTransform tooltipRect = UI_MotifylTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_MotifylTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_MotifylTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(tooltipRect.anchoredPosition.x, y);

        Cursor.visible = false;
    }

    void ExitSlotEvent(PointerEventData eventData)
    {
        UI_MotifylTip.Instance.SetColor(0f);

        Cursor.visible = true;
    }
}
