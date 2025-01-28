using Data;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Slot : UIPopup
{
    enum GameObjects
    {
        SlotIcon,
        ItemStack
    }

    public Iteminfo item;
    private RectTransform rect;


    public int Index { get; private set; }

    public override void Init()
    {
        rect = GetComponent<RectTransform>();
        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.ItemStack).SetActive(false);

        gameObject.BindEvent((evt) => { Managers.Inventory.SelectIndex = Index;});
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    public void SetInfo(Iteminfo info)
    {
        item = info;
        info = info ?? Managers.Data.ItemDict[199];

        Image slot = GetObject((int)GameObjects.SlotIcon).GetComponent<Image>();
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);

        ItemCounting(info);
    }

    public void ItemCounting(Iteminfo info)
    {
        if (info.isStack == true)
        {
            GetObject((int)GameObjects.ItemStack).SetActive(true);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{info.curStack}");
        }
        else
        {
            GetObject((int)GameObjects.ItemStack).SetActive(false);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = "1";
        }
    }

    public void SetIndex(int index) { Index = index; }

    private void EnterSlotEvent(PointerEventData eventData)
    {
        if (item == null)
            return;

        UI_InvenTip.Instance.SetColor(1f);
        UI_InvenTip.Instance.SetToolTip(item);

        RectTransform tooltipRect = UI_InvenTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float x = Mathf.Clamp(tooltipRect.anchoredPosition.x, -UI_InvenTip.Instance.parentRect.position.x + tooltipRect.rect.size.x, UI_InvenTip.Instance.parentRect.position.x - tooltipRect.rect.size.x / 2);
        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_InvenTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_InvenTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(x, y);

        //Cursor.visible = false;
    }

    private void ExitSlotEvent(PointerEventData eventData)
    {
        UI_InvenTip.Instance.SetColor(0f);

        //Cursor.visible = true;
    }

    
}

