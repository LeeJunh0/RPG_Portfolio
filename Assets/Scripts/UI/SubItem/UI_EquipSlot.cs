using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipSlot : UIPopup
{
    enum GameObjects
    {
        SlotIcon
    }

    public Iteminfo equipItem;
    public int index;

    public override void Init()
    {
        BindObject(typeof(GameObjects));
    }

    public void SetInfo(Iteminfo info)
    {       
        equipItem = info;
        GameObject go = GetObject((int)GameObjects.SlotIcon);
        if (go == null)
            return;

        Image slot = go.GetComponent<Image>();
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);
    }

    public void SetBefore(Texture2D image)
    {
        equipItem = null;

        Image slot = GetObject((int)GameObjects.SlotIcon).GetComponent<Image>();
        slot.sprite = Managers.UI.TextureToSprite(image);
    }
}
