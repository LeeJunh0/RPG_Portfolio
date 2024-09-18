using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetDragSlot : MonoBehaviour
{
    public static UI_SetDragSlot    instance;
    public List<Image>              hoverSlots; 
    public UI_Slot                  dragSlot;
    public Image                    slotImage;
    public Image                    icon;
    public bool                     isDraging;

    void Start()
    {
        instance = this;
        icon = GetComponent<Image>(); 
        slotImage = GetComponent<Image>();
        hoverSlots = new List<Image>();
    }

    public void DragSetIcon(Image _icon)
    {
        icon.sprite = _icon.sprite;
        SetColor(1f);
    }

    public void SetColor(float _alpha)
    {
        Color color = icon.color;
        color.a = _alpha;
        icon.color = color;
    }
}
