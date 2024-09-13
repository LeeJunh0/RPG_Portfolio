using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stat : UIPopup
{    
    enum GameObjects
    {
        UI_Frame
    }

    Stat level;
    Text levelText;

    public override void Init()
    {
        base.Init();
        
        Bind<GameObject>(typeof(GameObjects));
        level = Managers.Game.GetPlayer().GetComponent<Stat>();
        levelText = Util.FindChild(GetObject((int)GameObjects.UI_Frame), "Character_Level_Text").GetComponent<Text>();
    }

    private void Update()
    {
        levelText.text = string.Format($"{level.Level}");
    }
}
