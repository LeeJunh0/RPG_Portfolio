using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbodimentMotify : Motify
{
    protected int count = 1;

    public virtual void Embodiment(Transform pos) 
    {
        switch (skill.skillData.type)
        {
            case Define.ESkill.Projectile:
                ProjectileEmbodiment(pos);
                break;
            case Define.ESkill.AreaOfEffect:
                AreaEmbodiment(pos);
                break;
            default:
                break;
        }
    }

    public void AreaEmbodiment(Transform pos)
    {
        for (int i = 0; i < count; i++)
        {
            ExplosionSkill explosionSkill = skill as ExplosionSkill;
            GameObject go = Managers.Resource.Instantiate(explosionSkill.prefab);
            go.transform.position = skill.targetPos;

            go.transform.SetParent(skill.transform);
            objects.Add(go);
        }
    }

    public void ProjectileEmbodiment(Transform pos)
    {
        for (int i = 0; i < count; i++)
        {
            ProjectileSkill projectileSkill = skill as ProjectileSkill;
            GameObject go = Managers.Resource.Instantiate(projectileSkill.prefab);
            go.transform.position = pos.position;
            go.transform.forward = projectileSkill.GetDirection();

            Projectile projectile = go.GetComponent<Projectile>();
            projectile.hitVFX = projectileSkill.hitVFX;
            projectile.muzzleVFX = projectileSkill.muzzleVFX;

            go.transform.SetParent(skill.transform);
            objects.Add(go);
        }
    }

    public override void Execute(Skill _skill)
    {
        base.Execute(_skill);

        // targetPos로 바꿔야함 근데 Transform이 아니면 right가 맛탱이감 해결해야함 ㅇㅇ
        Embodiment(skill.transform);
    }
    public override void SetMana() { }
}
