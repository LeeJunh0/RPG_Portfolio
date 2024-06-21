using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Exit : UIPopup
{
    enum ExitButtons
    {
        YesButton,
        NoButton
    }

    public override void Init()
    {
        Bind<Button>(typeof(ExitButtons));

        GetButton((int)ExitButtons.YesButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Game OFF");
            Application.Quit();
        });
        GetButton((int)ExitButtons.NoButton).gameObject.BindEvent((evt) =>
        {
            Debug.Log("Game Continue");
            Managers.UI.ClosePopupUI();
        });
    }
}
