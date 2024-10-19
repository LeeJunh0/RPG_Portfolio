using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : InitializeMotify
{
    public SlashProjectile(ProjectileSkill refSkill) : base(refSkill) { }

    public override void SetProjectile()
    {
        elemental = Define.EProjectile_Elemental.Slash;

        base.SetProjectile();
    }

    public override void Execute() { SetProjectile(); }
}
