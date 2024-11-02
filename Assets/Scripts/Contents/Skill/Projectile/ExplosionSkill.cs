using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : Skill
{
    private void Awake()
    {
        skillData = new Data.SkillInfo(Managers.Data.SkillDict["ExplosionSkill"]);
    }
    public override void Execute()
    {
        base.Execute();
    }
}
