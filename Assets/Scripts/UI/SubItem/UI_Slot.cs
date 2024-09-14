using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Slot : UIBase
{
    // ��� ���Ե��� �⺻��ɵ�
    /*
        # Functions
        - Init()                  ���� �ʱ⼳��
        - SetInfo()               ���� ��ɼ���
        - SetEvents()             ���� �̺�Ʈ�Ҵ�      
        - OnPointerEnterSlot()    ���콺�� ���� ������
        - OnPointerClickSlot()    ���콺�� Ŭ�� ������
        - OnPointerExitSlot()     ���콺�� ��������
        - OnBeginDragSlot()       �巡�� �����ϴ� ����
        - OnDragSlot()            �巡�� ������
        - OnExitSlot()            �巡�� ������ ����
        - OnDragSlot()            �巡�װ� �� ������ ��������
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
