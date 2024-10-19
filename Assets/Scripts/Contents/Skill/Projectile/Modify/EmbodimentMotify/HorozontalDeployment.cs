using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDeployment : EmbodimentMotify
{
    public HorizontalDeployment(ProjectileSkill refSkill) : base(refSkill) { }

    public override void Embodiment(Transform pos)
    {
        int half = 3 / 2;
        for (int i = -half; i <= half; i++)
        {
            GameObject go = Managers.Resource.Instantiate(skill.projectile);
            go.transform.position = new Vector3(pos.position.x, 1f, pos.position.z) + pos.right * (i * 3f);
            go.transform.forward = pos.forward;

            Projectile projectile = go.GetComponent<Projectile>();
            projectile.hitVFX = skill.hitVFX;
            projectile.muzzleVFX = skill.muzzleVFX;

            go.transform.SetParent(skill.transform);
            projectiles.Add(go);
        }
    }

    public override void Execute()
    {
        Embodiment(Managers.Game.GetPlayer().transform);
    }
}
