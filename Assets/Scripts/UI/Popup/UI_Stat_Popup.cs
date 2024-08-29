using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat_Popup : UIPopup
{
    enum GameObjects
    {
        Level_Text,
        Att_Text,
        Hp_Text
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void StatPopupInit()
    {
        if (gameObject.activeSelf == false)
            return;

        Stat stat = Managers.Game.GetPlayer().GetComponent<Stat>();
        GetObject((int)GameObjects.Level_Text).GetComponent<Text>().text = string.Format($"Lvl  :   {stat.Level}");
        GetObject((int)GameObjects.Att_Text).GetComponent<Text>().text = string.Format($"Att  :   {stat.Attack}");
        GetObject((int)GameObjects.Hp_Text).GetComponent<Text>().text = string.Format($"Hp  :   {stat.MaxHp}");
    }
}
