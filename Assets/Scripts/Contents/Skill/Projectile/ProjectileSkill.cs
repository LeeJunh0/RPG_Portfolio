using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// �߻�ü ��ų���� �����θ�
public class ProjectileSkill : Skill
{
    protected EProjectile_Elemental elemental = EProjectile_Elemental.None;
    protected GameObject projectile;
    protected GameObject hitVFX;
    protected GameObject muzzleVFX;

    // �̰� ���߿� Ȯ�强 �ְ� �ٲܵ� ������ �Ӽ� �߰��Ҷ����� �׳� �� �밡�ٷ� �����;

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

    protected override void SkillInitialize() // ��ųʸ��� �ִ� �̸����� �������� �ε��� �޾ƿ�
    {
        projectile = Managers.Resource.Instantiate(projectileDict[elemental]);
        hitVFX = Managers.Resource.Instantiate(hitVFXDict[elemental]);
        muzzleVFX = Managers.Resource.Instantiate(muzzleVFXDict[elemental]);
    }

    protected override void SkillRealization()
    {
        
    }
}
