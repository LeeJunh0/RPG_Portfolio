using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Execute();
}

public abstract class Skill : ISkill
{
    public SkillData skillData;

    public ISkill initialize;
    public ISkill creating;
    public ISkill embodiment;
    public ISkill movement;

    public virtual void Execute() { }
    public void SetInitialize(ProjectileSkill skill, ISkill motify) { skill.initialize = motify; }
    public void SetCreating(ProjectileSkill skill, ISkill motify) { skill.creating = motify; }
    public void SetEmbodiment(ProjectileSkill skill, ISkill motify) { skill.embodiment = motify; }
    public void SetMovement(ProjectileSkill skill, ISkill motify) { skill.movement = motify; }
}
