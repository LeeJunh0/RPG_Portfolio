using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Motify : ISkill
{
    protected   Skill               skill;    
    protected   List<GameObject>    objects;
    protected   int                 mana;

    public virtual void Execute(Skill _skill) 
    {
        skill = _skill;
        objects = skill.objects;
    }
    public virtual void StopRun() { }
    public virtual void SetMana() { }
}
