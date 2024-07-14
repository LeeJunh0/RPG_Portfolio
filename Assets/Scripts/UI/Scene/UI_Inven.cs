using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        GridPanel,
        CreatButton
    }

    public override void Init()
    {
        //base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        GetObject((int)GameObjects.CreatButton).BindEvent((evt) =>
        {

        });
        foreach(Transform child in gridPanel.transform)        
            Managers.Resource.Destroy(child.gameObject);
        
        for(int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent : gridPanel.transform).gameObject;
            UI_Inven_Item invenitem = item.GetOrAddComponent<UI_Inven_Item>();
            invenitem.SetInfo($"집행검{i}번");
        }
    }
}
