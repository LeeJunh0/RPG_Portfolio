using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : UIPopup
{
    enum Buttons
    {
        PointButton,
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;

        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.EUiEvent.Drag);

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);
    }
    int score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        score++;

        GetText((int)Texts.ScoreText).text = $"���� : {score }";
    }
}
