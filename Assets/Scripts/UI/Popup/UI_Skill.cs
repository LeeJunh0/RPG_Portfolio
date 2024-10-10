using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Skill : UIPopup
{
    enum GameObjects
    {
        Initialize_MotifyGround,
        Embodiment_MotifyGround,
        Movement_MotifyGround
    }

    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));
        SetMotifys();
    }

    private void SetMotifys()
    {

    }
}
