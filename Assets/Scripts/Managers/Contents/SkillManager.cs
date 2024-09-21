using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    List<SkillInitialize> initializes;
    List<SkillRealization> realizations;
    List<SkillMovement> movements;

    public void Init()
    {
        initializes = new List<SkillInitialize>();
        realizations = new List<SkillRealization>();
        movements = new List<SkillMovement>();

        ObjectLink();
        ObjectRealization();
        ObjectMovement();
    }

    public void ObjectLink()
    {
        foreach (var skillInit in Managers.Data.SkillInitDict.Values)
        {
            SkillInitialize init = new SkillInitialize();
            init.magic = Managers.Resource.Load<GameObject>(skillInit.magicName);
            init.muzzle = Managers.Resource.Load<GameObject>(skillInit.muzzleName);
            init.hit = Managers.Resource.Load<GameObject>(skillInit.hitName);

            initializes.Add(init);
        }
    }

    public void ObjectRealization()
    {
        foreach(var skillInst in Managers.Data.SkillInstDict.Values)
        {
            SkillRealization realization = new SkillRealization();
            realization.Count = skillInst.count;
            realization.EType = (Define.ESkill)skillInst.type;

            realizations.Add(realization);
        }
    }

    public void ObjectMovement()
    {
        foreach (var skillmove in Managers.Data.SkillMoveDict.Values)
        {
            SkillMovement move = new SkillMovement();
            move.Speed = skillmove.speed;
            move.EType = (Define.ESkill)skillmove.type;

            movements.Add(move);
        }
    }

    public SkillInitialize NullInitialize() { return initializes[0]; }
    public SkillRealization NullRealization() { return realizations[0]; }
    public SkillMovement NullMoveMent() { return movements[0]; }

    public void SkillInitSet(int index) { Skill.skillInstance.tempMethod(initializes[index]); }
    public void SkillInstSet(int index) { Skill.skillInstance.realization = realizations[index]; }
    public void SkillMoveSet(int index) { Skill.skillInstance.movement = movements[index]; }
}
