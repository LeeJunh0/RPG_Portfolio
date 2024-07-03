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
    }

    string myName;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = myName;

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => 
        { 
            Debug.Log($"아이템 클릭! {myName}");
        });
    }

    public void SetInfo(string name)
    {
        myName = name;
    }
}
