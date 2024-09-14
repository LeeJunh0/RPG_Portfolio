using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DragSlot : MonoBehaviour
{
    // 마우스로 슬롯을 옮기는 과정을 보여주는 슬롯

    public static UI_DragSlot instance;
    public UI_Slot slot;

    public Image icon;

    private void Start() { instance = this; }

    public void DragSetImage(Image iconImage)
    {
        icon.sprite = iconImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = icon.color;
        color.a = _alpha;
        icon.color = color;
    }
}
