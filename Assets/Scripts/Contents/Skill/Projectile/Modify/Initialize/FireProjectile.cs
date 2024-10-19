using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : InitializeMotify
{
    public FireProjectile(ProjectileSkill refSkill) : base(refSkill) { }
    public override void SetProjectile()
    {
        elemental = Define.EProjectile_Elemental.Fire;

        base.SetProjectile();
    }

    public override void Execute() { SetProjectile(); }
}
