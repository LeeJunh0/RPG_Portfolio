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

        if (initialize == null) { initialize = new InitializeMotify(); }
        if (embodiment == null) { embodiment = new EmbodimentMotify(); }
        if (movement == null) { movement = new MoveMotify(); }

        initialize.Execute(this);
        embodiment.Execute(this);
        movement.Execute(this);
    }
}
