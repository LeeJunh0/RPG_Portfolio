using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat_MpBar : UIBase
{
    enum GameObjects
    {
        RatioText
    }

    Slider slider;
    Stat stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        stat = Managers.Game.GetPlayer().GetComponent<Stat>();
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        SetRatio(stat.Mp, stat.MaxMp);
    }

    private void SetRatio(float curMp, float MaxMp)
    {
        slider.value = curMp / MaxMp;

        GetObject((int)GameObjects.RatioText).GetComponent<Text>().text = string.Format($"{curMp} / {MaxMp}");
    }
}
