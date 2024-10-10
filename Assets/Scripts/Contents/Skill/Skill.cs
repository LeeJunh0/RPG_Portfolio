using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Execute();
}

public class Skill : MonoBehaviour
{
    public SkillInfo skillData;

    public InitializeMotify initialize;
    public EmbodimentMotify embodiment;
    public MoveMotify movement;

    public virtual void Execute() { }

    public void SetInitializeMotify(InitializeMotify motify)    { initialize = motify; }
    public void SetEmbodimentMotify(EmbodimentMotify motify)    { embodiment = motify; }
    public void SetMovementMotify(MoveMotify motify)            { movement = motify; }
}
