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

    [SerializeField]
    Data.Iteminfo myInfo;

    public Data.Iteminfo MyInfo
    {
        get 
        { 
            if(myInfo == null)
                myInfo = Managers.Data.ItemDict[101];

            return myInfo;
        }

        set
        {
            myInfo = value ?? Managers.Data.ItemDict[101];

            Texture2D texture = Managers.Resource.Load<Texture2D>(myInfo.uiInfo.icon);
            transform.Find("ItemIcon").GetComponent<Image>().sprite = Managers.UI.TextureToSprite(texture);
            GetObject((int)GameObjects.ItemNameText).GetComponent<Text>().text = myInfo.uiInfo.name;

            if (myInfo.uiInfo.isStack == true)
            {
                GetObject((int)GameObjects.ItemStack).SetActive(true);
                MyStack++;
            }
            else
            {
                myStack = 0;
                GetObject((int)GameObjects.ItemStack).SetActive(false);
            }
        }
    }

    int myStack = 0;
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
        GetObject((int)GameObjects.ItemIcon).BindEvent((click) =>
        {
            Debug.Log($"선택된 아이템이름 : {MyInfo.uiInfo.name}");
            Managers.Inventory.CurItem = this;
        });
    }
}

