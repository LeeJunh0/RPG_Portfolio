using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Motify : ISkill
{
    protected ProjectileSkill   skill; 
    protected List<GameObject>  projectiles;
    protected int               mana;

    public Motify(ProjectileSkill refSkill) 
    { 
        this.skill = refSkill;
        projectiles = skill.projectiles;
    }

    public virtual void SetMana() { }
    public virtual void Execute() { skill.Execute(); }
}
