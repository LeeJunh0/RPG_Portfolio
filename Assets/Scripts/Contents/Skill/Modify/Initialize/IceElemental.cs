using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceElemental : InitializeMotify
{
    public override void SetElemental() 
    {
        elemental = Define.ESkill_Elemental.Ice;

        base.SetElemental();
    }

    public override void Execute(Skill _skill) 
    {
        base.Execute(_skill);

        SetElemental(); 
    }
}
