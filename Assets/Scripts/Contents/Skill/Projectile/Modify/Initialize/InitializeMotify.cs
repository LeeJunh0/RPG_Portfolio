using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static UnityEditor.Rendering.FilterWindow;

public class InitializeMotify : Motify
{
    public EProjectile_Elemental    elemental = EProjectile_Elemental.None;

    protected Dictionary<EProjectile_Elemental, string> projectileDict = new Dictionary<EProjectile_Elemental, string>()
    {
        { EProjectile_Elemental.None,   "NormalProjectile" },
        { EProjectile_Elemental.Fire,   "FireProjectile" },
        { EProjectile_Elemental.Ice,    "IceProjectile" },
        { EProjectile_Elemental.Slash,  "SlashProjectile" }
    };
    protected Dictionary<EProjectile_Elemental, string> hitVFXDict = new Dictionary<EProjectile_Elemental, string>()
    {
        { EProjectile_Elemental.None,   "NormalHit" },
        { EProjectile_Elemental.Fire,   "FireHit" },
        { EProjectile_Elemental.Ice,    "IceHit" },
        { EProjectile_Elemental.Slash,  "SlashHit" }
    };
    protected Dictionary<EProjectile_Elemental, string> muzzleVFXDict = new Dictionary<EProjectile_Elemental, string>()
    {
        { EProjectile_Elemental.None,   "NormalMuzzle" },
        { EProjectile_Elemental.Fire,   "FireMuzzle" },
        { EProjectile_Elemental.Ice,    "IceMuzzle" },
        { EProjectile_Elemental.Slash,  "SlashMuzzle" }
    };

    public InitializeMotify(ProjectileSkill refSkill) : base(refSkill) { }

    public virtual void SetProjectile()
    {
        skill.projectile = Managers.Resource.Load<GameObject>(projectileDict[elemental]);
        skill.hitVFX = Managers.Resource.Load<GameObject>(hitVFXDict[elemental]);
        skill.muzzleVFX = Managers.Resource.Load<GameObject>(muzzleVFXDict[elemental]);       
    }

    public override void Execute()
    {
        SetProjectile();
    }
}
