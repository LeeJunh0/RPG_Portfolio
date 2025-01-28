using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Pause : UIPopup
{
    enum Buttons
    {
        YesButton,
        NoButton
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(Buttons));

        GetObject((int)Buttons.YesButton).BindEvent((PointerEventData) =>
        {
            Debug.Log("Change TitleScene ..");
            UILoading loader = Managers.UI.ShowPopupUI<UILoading>("UI_LoadingScreen");
            loader.Loading(Define.EScene.Title);         
        }); 
        GetObject((int)Buttons.NoButton).BindEvent((PointerEventData) =>
        {
            Managers.Resource.Destroy(this.gameObject);
        });
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
