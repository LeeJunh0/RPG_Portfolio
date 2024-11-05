using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Define;
using static UnityEditor.Rendering.FilterWindow;

public class InitializeMotify : Motify
{
    public ESkill_Elemental    elemental = ESkill_Elemental.None;

    protected Dictionary<ESkill_Elemental, string> projectileDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None,   "NormalProjectile" },
        { ESkill_Elemental.Fire,   "FireProjectile" },
        { ESkill_Elemental.Ice,    "IceProjectile" },
        { ESkill_Elemental.Wind,  "WindProjectile" }
    };
    protected Dictionary<ESkill_Elemental, string> hitVFXDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None,   "NormalHit" },
        { ESkill_Elemental.Fire,   "FireHit" },
        { ESkill_Elemental.Ice,    "IceHit" },
        { ESkill_Elemental.Wind,  "WindHit" }
    };
    protected Dictionary<ESkill_Elemental, string> muzzleVFXDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None,   "NormalMuzzle" },
        { ESkill_Elemental.Fire,   "FireMuzzle" },
        { ESkill_Elemental.Ice,    "IceMuzzle" },
        { ESkill_Elemental.Wind,  "WindMuzzle" }
    };
    protected Dictionary<ESkill_Elemental, string> areaDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None, "NormalArea" },
        { ESkill_Elemental.Fire, "FireArea" },
        { ESkill_Elemental.Ice,"IceArea" },
        { ESkill_Elemental.Wind,"WindArea" }
    };
    public virtual void SetElemental()
    {
        switch (skill.skillData.type)
        {
            case ESkill.Projectile:
                {
                    ProjectileSkill projectileSkill = skill as ProjectileSkill;
                    projectileSkill.prefab = Managers.Resource.Load<GameObject>(projectileDict[elemental]);
                    projectileSkill.hitVFX = Managers.Resource.Load<GameObject>(hitVFXDict[elemental]);
                    projectileSkill.muzzleVFX = Managers.Resource.Load<GameObject>(muzzleVFXDict[elemental]);
                }
                break;
            case ESkill.AreaOfEffect:
                {
                    ExplosionSkill explosionSkill = skill as ExplosionSkill;
                    explosionSkill.prefab = Managers.Resource.Load<GameObject>(areaDict[elemental]);
                }
                break;
            default:
                break;
        }
    }

    public override void Execute(Skill _skill)
    {
        base.Execute(_skill);

        SetElemental();
    }
}
