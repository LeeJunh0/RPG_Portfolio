using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// 발사체 스킬들의 상위부모
public class ProjectileSkill : Skill
{
    protected EProjectile_Elemental elemental = EProjectile_Elemental.None;
    protected GameObject projectile;
    protected GameObject hitVFX;
    protected GameObject muzzleVFX;

    // 이건 나중에 확장성 있게 바꿀듯 지금은 속성 추가할때마다 그냥 다 노가다로 써야함;

    Dictionary<EProjectile_Elemental, string> projectileDict = new Dictionary<EProjectile_Elemental, string>()
    {
        { EProjectile_Elemental.None,   "NormalProjectile" },
        { EProjectile_Elemental.Fire,   "FireProjectile" },
        { EProjectile_Elemental.Ice,    "IceProjectile" },
        { EProjectile_Elemental.Slash,  "SlashProjectile" }
    };
    Dictionary<EProjectile_Elemental, string> hitVFXDict = new Dictionary<EProjectile_Elemental, string>()
    {
        { EProjectile_Elemental.None,   "NormalHit" },
        { EProjectile_Elemental.Fire,   "FireHit" },
        { EProjectile_Elemental.Ice,    "IceHit" },
        { EProjectile_Elemental.Slash,  "SlashHit" }
    };
    Dictionary<EProjectile_Elemental, string> muzzleVFXDict = new Dictionary<EProjectile_Elemental, string>()
    {
        { EProjectile_Elemental.None,   "NormalMuzzle" },
        { EProjectile_Elemental.Fire,   "FireMuzzle" },
        { EProjectile_Elemental.Ice,    "IceMuzzle" },
        { EProjectile_Elemental.Slash,  "SlashMuzzle" }
    };

    protected override void SkillInitialize() // 딕셔너리에 있는 이름으로 프리팹을 로드해 받아옴
    {
        projectile = Managers.Resource.Instantiate(projectileDict[elemental]);
        hitVFX = Managers.Resource.Instantiate(hitVFXDict[elemental]);
        muzzleVFX = Managers.Resource.Instantiate(muzzleVFXDict[elemental]);
    }

    protected override void SkillRealization()
    {
        
    }
}
