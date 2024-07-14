using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        GridPanel,
        CreateButton,
        DeleteButton
    }

    GameObject inven;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        inven = Util.FindChild(gridPanel, "Content", true);
        GetObject((int)GameObjects.CreateButton).BindEvent((evt) => { CreateItem(); });
        GetObject((int)GameObjects.DeleteButton).BindEvent((evt) => { DeleteItem(); });

        foreach (Transform child in gridPanel.transform)        
            Managers.Resource.Destroy(child.gameObject);
    }

    void CreateItem()
    {
        UI_Inven_Item item = Managers.UI.MakeSubItem<UI_Inven_Item>(inven.transform);
    }
    void DeleteItem() 
    { 

    }
}
