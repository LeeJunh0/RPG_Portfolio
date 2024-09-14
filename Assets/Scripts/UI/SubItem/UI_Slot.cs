using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Slot : UIBase
{
    // 모든 슬롯들의 기본기능들
    /*
        # Functions
        - Init()                  슬롯 초기설정
        - SetInfo()               슬롯 기능설정
        - SetEvents()             슬롯 이벤트할당      
        - OnPointerEnterSlot()    마우스가 위에 있을때
        - OnPointerClickSlot()    마우스가 클릭 했을때
        - OnPointerExitSlot()     마우스가 나갔을때
        - OnBeginDragSlot()       드래그 시작하는 순간
        - OnDragSlot()            드래그 진행중
        - OnExitSlot()            드래그 끝나는 순간
        - OnDragSlot()            드래그가 내 위에서 끝났을때
     */

    enum IconImage { Slot_Image, }

    public Image icon;
    public Image nullIcon;

    public override void Init()
    {
        SetEvents();
        BindImage(typeof(IconImage));
    }

    public void SetEvents()
    {
        gameObject.BindEvent((evt) => { OnPointerEnterSlot(evt); });
        gameObject.BindEvent((evt) => { OnPointerClickSlot(evt); });
        gameObject.BindEvent((evt) => { OnPointerExitSlot(evt); });
        gameObject.BindEvent((evt) => { OnBeginDragSlot(evt); });
        gameObject.BindEvent((evt) => { OnDragSlot(evt); });
        gameObject.BindEvent((evt) => { OnEndDragSlot(evt); });
        gameObject.BindEvent((evt) => { OnDropSlot(evt); });
    }

    protected virtual void OnPointerEnterSlot(PointerEventData eventData) { }
    protected virtual void OnPointerClickSlot(PointerEventData eventData) { }
    protected virtual void OnPointerExitSlot(PointerEventData eventData) { }
    protected virtual void OnBeginDragSlot(PointerEventData eventData) { }
    protected virtual void OnDragSlot(PointerEventData eventData) { }
    protected virtual void OnEndDragSlot(PointerEventData eventData) { }
    protected virtual void OnDropSlot(PointerEventData eventData) { }
}
