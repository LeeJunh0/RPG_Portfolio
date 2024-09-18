using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Slot : UIBase
{
    protected enum Images { SlotIcon, }
    public Image icon;

    public override void Init()
    {
        SetInfo();
        SetEvents();
    }

    public virtual void SetInfo()
    {
        BindImage(typeof(Images));
    }

    public void SetIcon(Sprite _icon) { icon.sprite = _icon; }

    public void SetEvents()
    {
        gameObject.BindEvent((evt) => { OnPointerEnter(evt); },         Define.EUiEvent.PointerEnter); 
        gameObject.BindEvent((evt) => { OnPointerExit(evt); },          Define.EUiEvent.PointerExit);
        gameObject.BindEvent((evt) => { OnBeginDrag(evt); },            Define.EUiEvent.BeginDrag);
        gameObject.BindEvent((evt) => { OnDrag(evt); },                 Define.EUiEvent.Drag);
        gameObject.BindEvent((evt) => { OnEndDrag(evt); },              Define.EUiEvent.EndDrag);
        gameObject.BindEvent((evt) => { OnClick(evt); },                Define.EUiEvent.Click);
        gameObject.BindEvent((evt) => { OnDrop(evt); },                 Define.EUiEvent.Drop);
        gameObject.BindEvent((evt) => { OnPointerUp(evt); },            Define.EUiEvent.Up);
    }

    protected virtual void OnPointerEnter(PointerEventData eventData) { }
    protected virtual void OnPointerExit(PointerEventData eventData) { }
    protected virtual void OnBeginDrag(PointerEventData eventData) { }
    protected virtual void OnDrag(PointerEventData eventData) { }
    protected virtual void OnEndDrag(PointerEventData eventData) { }
    protected virtual void OnClick(PointerEventData eventData) {}
    protected virtual void OnDrop(PointerEventData eventData) { }
    protected virtual void OnPointerUp(PointerEventData eventData) { }

}
