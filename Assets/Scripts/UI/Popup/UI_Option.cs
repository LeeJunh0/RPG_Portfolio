using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : UIPopup
{
    Slider bgmBar;
    Slider effectBar;

    enum Option_Sliders
    {
        BGM_Slider,
        Effect_Slider
    }

    enum Option_Buttons
    {
        Option_ExitButton
    }

    public override void Init()
    {
        Bind<Slider>(typeof(Option_Sliders));
        Bind<Button>(typeof(Option_Buttons));

        GetButton((int)Option_Buttons.Option_ExitButton).gameObject.BindEvent((evt) =>
        {
            Managers.UI.ClosePopupUI();
        });

        bgmBar = GetSlider((int)Option_Sliders.BGM_Slider);
        effectBar = GetSlider((int)Option_Sliders.Effect_Slider);

        bgmBar.onValueChanged.AddListener((evt) =>
        {
            Managers.Sound.GetAudioSource(Define.ESound.Bgm).volume = bgmBar.value;
        });
        effectBar.onValueChanged.AddListener((evt) =>
        {
            Managers.Sound.GetAudioSource(Define.ESound.Effect).volume = effectBar.value;
        });
    }
}
