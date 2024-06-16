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

    GameObject startButton;
    GameObject optionButton;
    GameObject exitButton;

    public override void Init()
    { 
        Bind<Button>(typeof(GameObjects));

        startButton = GetButton((int)GameObjects.StartButton).gameObject;
        optionButton = GetButton((int)GameObjects.OptionButton).gameObject;
        exitButton = GetButton((int)GameObjects.ExitButton).gameObject;

        GetButton((int)GameObjects.StartButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Change Scene ..");
            //TODO  - 비동기로딩
            
            UILoading loader = Managers.Resource.Instantiate("UI_LoadingScreen").GetOrAddComponent<UILoading>();
            loader.Loading(Define.Scene.Game);
        });
        GetButton((int)GameObjects.OptionButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Option Screen");
        });
        GetButton((int)GameObjects.ExitButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Game Off");
        });

        startButton.SetActive(false);
        optionButton.SetActive(false);
        exitButton.SetActive(false);

        StartLoad();
    }

    void StartLoad()
    {
        Debug.Log($"Title Loading!");
        Managers.Resource.LoadAllAsync<Object>("Title", (key, count, total) =>
        {
            Debug.Log($"{key} {count}/{total}");
            if(count >= total)
            {
                Managers.Data.Init();

                startButton.SetActive(true);
                optionButton.SetActive(true);
                exitButton.SetActive(true);
            }
        });
    }
}
