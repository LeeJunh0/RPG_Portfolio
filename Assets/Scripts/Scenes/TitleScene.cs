using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        StartLoad();       
    }

    public override void Clear()
    {
        Debug.Log("TitleScene Clear!");
    }

    void StartLoad()
    {
        Debug.Log($"Title Loading!");
        Managers.Resource.LoadAllAsync<Object>("Title", (key, count, total) =>
        {
            Debug.Log($"{key} {count}/{total}");
            if (count >= total)
            {
                Managers.Data.Init();
                Managers.ESound.Play("Happy", Define.ESound.Bgm); 
                Managers.UI.ShowPopupUI<UI_TitleScene>();
            }
        });
        
    }
}
