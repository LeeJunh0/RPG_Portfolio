using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        SortingButton
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.SortingButton).BindEvent((evt) => { Debug.Log("정렬버튼 on"); });
    }

}
