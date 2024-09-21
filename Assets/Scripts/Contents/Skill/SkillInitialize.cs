using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInitialize : SkillMaker
{
    public GameObject       magic;
    public GameObject       muzzle;
    public GameObject       hit;

    // ������ �Ӽ�����
    public override void SkillInit() 
    {
        magic.GetComponent<MagicBall>().hitVFX = hit;
        magic.GetComponent<MagicBall>().muzzleVFX = muzzle;
        Skill.skillInstance.prefabMagic = magic;
    }
}
