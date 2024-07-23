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

    Data.Iteminfo myInfo;

    public Data.Iteminfo MyInfo
    {
        get { return myInfo == null ? myInfo : Managers.Data.ItemDict[101]; }

        set
        {
            if(value == null)            
                value = Managers.Data.ItemDict[101];
            
            Texture2D texture = Managers.Resource.Load<Texture2D>(value.uiInfo.icon);
            GetObject((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = Managers.UI.TextureToSprite(texture);
            GetObject((int)GameObjects.ItemNameText).GetComponent<Text>().text = value.uiInfo.name;

            if (value.uiInfo.isStack == true)
            {
                GetObject((int)GameObjects.ItemStack).SetActive(true);
                GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{MyStack}");
            }
        }
    }

    int myStack = 1;
    public int MyStack
    {
        get { return myStack; }

        set
        {
            myStack = value;
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{myStack}");
        }
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.ItemStack).SetActive(false);
        GetObject((int)GameObjects.ItemIcon).BindEvent((PointerEventData) =>
        {
            Debug.Log($"아이템 클릭! {MyInfo.uiInfo.name}");
        });
    }
}

