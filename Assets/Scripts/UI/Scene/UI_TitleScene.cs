using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScene : UIBase
{
    enum GameObjects
    {
        StartButton,
        OptionButton,
        ExitButton,
    }

    public override void Init()
    {
        StartLoad();

        Bind<Button>(typeof(GameObjects));
        GetButton((int)GameObjects.StartButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Change Scene ..");
            //TODO  - 비동기로딩
            Managers.Scene.LoadScene(Define.Scene.Game);    
        });
        GetButton((int)GameObjects.OptionButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Option Screen");
        });
        GetButton((int)GameObjects.ExitButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Game Off");
        });
    }

    void StartLoad()
    {
        Managers.Resource.LoadAllAsync<Object>("Title", (key, count, total) =>
        {
            Debug.Log($"{key} {count}/{total}");

            if(count >= total)
            {
                GetObject((int)GameObjects.StartButton).SetActive(true);
                GetObject((int)GameObjects.OptionButton).SetActive(true);                
                GetObject((int)GameObjects.OptionButton).SetActive(true);
            }
        });
    }
}
