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

    Data.Iteminfo myInfo;
    public Data.Iteminfo GetInfo()
    {
        if (myInfo == null)
            myInfo = Managers.Data.ItemDict[199];

        return myInfo;
    }

    public void SetInfo(Data.Iteminfo value)
    {
        myInfo = value ?? Managers.Data.ItemDict[199];

        Texture2D texture = Managers.Resource.Load<Texture2D>(myInfo.uiInfo.icon);
        transform.Find("ItemIcon").GetComponent<Image>().sprite = Managers.UI.TextureToSprite(texture);
        GetObject((int)GameObjects.ItemNameText).GetComponent<Text>().text = myInfo.uiInfo.name;

        if (myInfo.uiInfo.isStack == true)
        {
            GetObject((int)GameObjects.ItemStack).SetActive(true);
            SetStack(myInfo.myStack++);
        }
        else
        {
            SetStack(0);
            GetObject((int)GameObjects.ItemStack).SetActive(false);
        }
    }

    public int GetStack() 
    { 
        return myInfo.myStack; 
    }

    public void SetStack(int value)
    {
        myInfo.myStack = value;
        GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{myInfo.myStack}");
    }


    public int Index { get; private set; }

    public void SetIndex(int index) { Index = index; }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.ItemStack).SetActive(false);
        GetObject((int)GameObjects.ItemIcon).BindEvent((click) =>
        {
            Debug.Log($"선택된 아이템이름 : {myInfo.uiInfo.name}");
            Managers.Inventory.CurItemIndex = Index;
            Debug.Log($"선택된 Index : {Index}");
        });
    }

    public void InfoUpdate()
    {

    }
}

