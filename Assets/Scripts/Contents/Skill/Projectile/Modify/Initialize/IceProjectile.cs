using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : InitializeMotify
{
    public IceProjectile(ProjectileSkill refSkill) : base(refSkill) { }

    public override void SetProjectile() 
    {
        elemental = Define.EProjectile_Elemental.Ice;

        base.SetProjectile();
    }

    public override void Execute()
    {
        base.Execute();

        SetProjectile();
    }
}
