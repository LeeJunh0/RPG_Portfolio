using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DragSlot : MonoBehaviour
{
    // ���콺�� ������ �ű�� ������ �����ִ� ����

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
