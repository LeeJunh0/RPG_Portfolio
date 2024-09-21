using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInitialize : SkillMaker
{
    public GameObject       magic;
    public GameObject       muzzle;
    public GameObject       hit;

    // 마법의 속성결정
    public override void SkillInit() 
    {
        magic.GetComponent<MagicBall>().hitVFX = hit;
        magic.GetComponent<MagicBall>().muzzleVFX = muzzle;
        Skill.skillInstance.prefabMagic = magic;
    }
}
