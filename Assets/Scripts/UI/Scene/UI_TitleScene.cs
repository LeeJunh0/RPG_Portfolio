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

        startButton.BindEvent((evt) =>
        {
            Debug.Log("Change GameScene ..");
            UILoading loader = Managers.UI.ShowPopupUI<UILoading>("UI_LoadingScreen");
            loader.Loading(Define.EScene.Game);
        });
        optionButton.BindEvent((evt) =>
        {
            Debug.Log("Option Screen");
            Managers.UI.ShowPopupUI<UI_Option>("UI_Option");
        });
        exitButton.BindEvent((evt) =>
        {
            Debug.Log("Game Off");
            Managers.UI.ShowPopupUI<UI_Exit>("UI_ExitScreen");
        });
    }
}
