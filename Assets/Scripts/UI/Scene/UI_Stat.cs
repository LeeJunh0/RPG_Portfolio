using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat : UIPopup
{    
    enum GameObjects
    {
        OpenPopup_Button,
        UI_Stat_Popup
    }

    GameObject popup;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        popup = GetObject((int)GameObjects.UI_Stat_Popup);
        popup.SetActive(false);

        GetObject((int)GameObjects.OpenPopup_Button).BindEvent((evt) =>
        {
            popup.SetActive(!popup.activeSelf);
            popup.GetOrAddComponent<UI_Stat_Popup>().StatPopupInit();
        });
    }
}
