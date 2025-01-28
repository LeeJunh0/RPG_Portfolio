using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Trader : UIPopup
{
    enum GameObjects
    {
        UI_Trader_ItemList,
        UI_Trader_ExitButton
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.UI_Trader_ExitButton).BindEvent(evt =>
        {
            Managers.UI.ClosePopupUI();
        });

        ItemListCreate();
    }

    public void ItemListCreate()
    {
        GameObject list = GetObject((int)GameObjects.UI_Trader_ItemList);
        for (int i = 0; i < Managers.Data.ItemDict.Count - 1; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Trader_Item>(parent: list.transform).gameObject;
            UI_Trader_Item traderItem = item.GetOrAddComponent<UI_Trader_Item>();
            item.transform.localScale = Vector3.one;

            traderItem.ItemInit(Managers.Data.ItemDict[102 + i]);
        }
    }
}
