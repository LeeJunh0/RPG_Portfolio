using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMovement : SkillMaker
{
    Define.ESkill type = Define.ESkill.Normal;
    float speed = 1f;

    public float Speed { get { return speed; } set { speed = value; } }
    public Define.ESkill EType { get { return type; } set { type = value; } }

}
