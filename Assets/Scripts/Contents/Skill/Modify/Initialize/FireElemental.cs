using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElemental : InitializeMotify
{
    public override void SetElemental()
    {
        elemental = Define.ESkill_Elemental.Fire;

        base.SetElemental();
    }

    public override void Execute(Skill _skill)
    {
        base.Execute(_skill);

        SetElemental();
    }

}
