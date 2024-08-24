using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UIBase
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
        ItemStack
    }

    public int Index { get; private set; }

    public void SetIndex(int index) { Index = index; }

    public void SetInfo(Data.Iteminfo value)
    {
        value = value ?? Managers.Data.ItemDict[199];

        Texture2D texture = Managers.Resource.Load<Texture2D>(value.uiInfo.icon);
        transform.Find("ItemIcon").GetComponent<Image>().sprite = Managers.UI.TextureToSprite(texture);
        GetObject((int)GameObjects.ItemNameText).GetComponent<Text>().text = value.uiInfo.name;

        if (value.uiInfo.isStack == true)
        {
            GetObject((int)GameObjects.ItemStack).SetActive(true);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{value.MyStack}");
        }
        else
        {
            GetObject((int)GameObjects.ItemStack).SetActive(false);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = "1";
        }
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.ItemStack).SetActive(false);
        GetObject((int)GameObjects.ItemIcon).BindEvent((click) =>
        {
            Debug.Log($"아이콘 Index : {Index}");
            Managers.Inventory.SelectIndex = Index;
            UI_Inven inven = FindObjectOfType<UI_Inven>();
            inven?.OnInvenPopup(Index);
        });
    }
}

