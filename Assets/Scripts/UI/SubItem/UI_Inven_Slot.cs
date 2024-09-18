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

    public int Index { get; private set; }

    public override void Init()
    {
        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.ItemStack).SetActive(false);
    }

    public void SetInfo(Iteminfo item)
    {
        item = item ?? Managers.Data.ItemDict[199];

        Image slot = GetObject((int)GameObjects.SlotIcon).GetComponent<Image>();
        Texture2D texture = Managers.Resource.Load<Texture2D>(item.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);

        ItemCounting(item);
    }

    public void ItemCounting(Iteminfo item)
    {
        if (item.isStack == true)
        {
            GetObject((int)GameObjects.ItemStack).SetActive(true);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{item.MyStack}");
        }
        else
        {
            GetObject((int)GameObjects.ItemStack).SetActive(false);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = "1";
        }
    }

    public void SetIndex(int index) { Index = index; }
}

