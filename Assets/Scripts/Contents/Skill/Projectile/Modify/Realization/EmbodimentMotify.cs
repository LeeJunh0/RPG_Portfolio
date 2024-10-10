using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbodimentMotify : Motify
{
    public EmbodimentMotify(ProjectileSkill refSkill) : base(refSkill) { }

    public virtual void Embodiment(Transform pos) 
    {
        for(int i = 0; i < skill.count; i++)
        {
            GameObject go = Managers.Resource.Instantiate(skill.projectile);
            go.transform.position = pos.position;
            go.transform.forward = Managers.Game.GetPlayer().transform.forward;

            Projectile projectile = go.GetComponent<Projectile>();
            projectile.hitVFX = skill.hitVFX;
            projectile.muzzleVFX = skill.muzzleVFX;

            skill.projectiles.Add(go);
        }
    }

    public override void SetMana() { }

    public override void Execute()
    {
        Embodiment(skill.transform);
    }
}
