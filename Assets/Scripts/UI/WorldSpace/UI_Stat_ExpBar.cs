using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat_ExpBar : UIBase
{
    enum GameObjects
    {
        RatioText
    }

    Slider slider;
    PlayerStat stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
    }

    private void Start()
    {
        slider = GetComponent<Slider>();        
    }

    private void Update()
    {
        SetValue(stat.Exp, stat.GetTotalExp());
    }

    private void SetValue(float curExp, float totalExp)
    {
        slider.maxValue = totalExp;
        slider.value = curExp;

        if(curExp == 0)
        {
            GetObject((int)GameObjects.RatioText).GetComponent<Text>().text = string.Format($"{0.00}%");
            return;
        }

        GetObject((int)GameObjects.RatioText).GetComponent<Text>().text = string.Format($"{(float)Math.Round(curExp / totalExp * 100, 2)}%");
    }

    
}
