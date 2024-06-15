using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Clear()
    {
        Debug.Log("TitleScene Clear!");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Title;
    }
}
