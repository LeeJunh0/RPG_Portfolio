using Data;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Popup : UIScene
{
    enum InvenPopupObject
    {
        PopupIcon,
        PopupNameText,
        PopupDescription,
        PopupButton
    }


    public override void Init()
    {
        Bind<GameObject>(typeof(InvenPopupObject));
    }

    public void InvenPopupInit(Iteminfo itemInfo)
    {
        if (itemInfo == null) 
            return;

        gameObject.SetActive(true);
        Texture2D texture = Managers.Resource.Load<Texture2D>(itemInfo.uiInfo.icon);
        transform.Find("PopupIcon").GetComponent<Image>().sprite = Managers.UI.TextureToSprite(texture);
        GetObject((int)InvenPopupObject.PopupNameText).GetComponent<Text>().text = itemInfo.uiInfo.name;
        GetObject((int)InvenPopupObject.PopupDescription).GetComponent<Text>().text = itemInfo.uiInfo.description;

        GetObject((int)InvenPopupObject.PopupButton).BindEvent((evt) =>
        {

        });
    }
}
