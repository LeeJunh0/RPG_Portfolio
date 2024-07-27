using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene : UIBase
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

    public void OnActive()
    {
        this.gameObject.SetActive(!gameObject.activeSelf);
    }
}
