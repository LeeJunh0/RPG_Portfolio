using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene : UIBase
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

    public void StateChange()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
