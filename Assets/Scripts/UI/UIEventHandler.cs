using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler, IBeginDragHandler ,IDragHandler,IEndDragHandler,IDropHandler
{
    public Action<PointerEventData> OnEnterHandler;
    public Action<PointerEventData> OnExitHandler;
    public Action<PointerEventData> OnClickHandler;
    public Action<PointerEventData> OnBeginDragHandler;
    public Action<PointerEventData> OnDragHandler;
    public Action<PointerEventData> OnEndDragHandler;
    public Action<PointerEventData> OnDropHandler;

    public void OnPointerEnter(PointerEventData eventData) { OnEnterHandler?.Invoke(eventData); }
    public void OnPointerClick(PointerEventData eventData) { OnClickHandler?.Invoke(eventData); }
    public void OnPointerExit(PointerEventData eventData) { OnExitHandler?.Invoke(eventData); }
    public void OnBeginDrag(PointerEventData eventData) { OnBeginDragHandler?.Invoke(eventData); }
    public void OnDrag(PointerEventData eventData) { OnDragHandler?.Invoke(eventData); }
    public void OnEndDrag(PointerEventData eventData) { OnEndDragHandler?.Invoke(eventData); }
    public void OnDrop(PointerEventData eventData) { OnDropHandler?.Invoke(eventData); }

}
