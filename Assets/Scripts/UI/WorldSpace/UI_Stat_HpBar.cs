using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat_HpBar : UIBase
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
        SetRatio(stat.Hp, stat.MaxHp);
    }

    private void SetRatio(float curHp, float MaxHp)
    {
        slider.value = curHp / MaxHp;

        GetObject((int)GameObjects.RatioText).GetComponent<Text>().text = string.Format($"{curHp} / {MaxHp}");
    }
}
