using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class HorizontalDeployment : EmbodimentMotify
{    
    public override void Embodiment(Transform pos)
    {
        count = 3;

        int half = count / 2;
        for (int i = -half; i <= half; i++)
        {
            ProjectileSkill projectileSkill = skill as ProjectileSkill;
            GameObject go = Managers.Resource.Instantiate(projectileSkill.prefab);
            go.transform.position = new Vector3(pos.position.x, 1f, pos.position.z) + pos.right * (i * 1f);
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
    }
}
