using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creating : Motify
{
    protected int count = 1;

    public Creating(ProjectileSkill refSkill) : base(refSkill) { }
    public int Count { set { count = value; } }
    public override void SetMana() { skill.skillData.Mana += 5f; }

    public virtual void Create(GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject projectile = Managers.Resource.Instantiate(prefab); 

            Projectile setting = skill.projectile.GetComponent<Projectile>();
            setting.muzzleVFX = skill.muzzleVFX;
            setting.hitVFX = skill.hitVFX;

            projectiles.Add(projectile);
        }
    }

    public override void Execute()
    {
        base.Execute();

        SetMana();
        Create(skill.projectile);
    }
}
