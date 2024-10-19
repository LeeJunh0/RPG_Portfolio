using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager
{
    public SkillInventory playerInventory;

    public void GetSkillInventory() { playerInventory = Managers.Game.GetPlayer().GetComponent<SkillInventory>(); }

    public Motify SetMotify(string name)
    {
        switch (name)
        {
            case "IceProjectile":
                return new IceProjectile(playerInventory.mySkills[0] as ProjectileSkill);
            case "SlashProjectile":
                return new SlashProjectile(playerInventory.mySkills[0] as ProjectileSkill);
            case "FireProjectile":
                return new FireProjectile(playerInventory.mySkills[0] as ProjectileSkill);
            case "EmbodimentMotify":
                return new EmbodimentMotify(playerInventory.mySkills[0] as ProjectileSkill);
            case "HorizontalDeployment":
                return new HorizontalDeployment(playerInventory.mySkills[0] as ProjectileSkill);
            case "DefenceDeployment":
                break;
            case "MoveMotify":
                return new MoveMotify(playerInventory.mySkills[0] as ProjectileSkill);
            case "CircleMove":
                return new CircleMove(playerInventory.mySkills[0] as ProjectileSkill);
        }
        return null;
    }
}
