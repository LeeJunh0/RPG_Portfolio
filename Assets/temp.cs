using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : UIBase
{
    enum Buttons 
    { 
        InitButton,
        InstButton,
        MoveButton
    }

    public int initIndex = 0;
    public int InstIndex = 0;
    public int MoveIndex = 0;

    public override void Init()
    {
        BindObject(typeof(Buttons));

        GetObject((int)Buttons.InitButton).BindEvent((evt) => 
        {
            if (initIndex >= 4)
                initIndex = 0;

            Managers.Skill.SkillInitSet(initIndex++); 
        });
        GetObject((int)Buttons.InstButton).BindEvent((evt) =>
        {
            if (InstIndex >= 3)
                InstIndex = 0;

            Managers.Skill.SkillInstSet(InstIndex++);
        }); GetObject((int)Buttons.MoveButton).BindEvent((evt) =>
        {
            if (MoveIndex >= 3)
                MoveIndex = 0;

            Managers.Skill.SkillMoveSet(MoveIndex++);
        });
    }
}
