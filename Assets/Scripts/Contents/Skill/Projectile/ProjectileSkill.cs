using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Define;

public class ProjectileSkill : Skill
{
    public GameObject       hitVFX;
    public GameObject       muzzleVFX;
    public int              count = 1;

    Rigidbody rigid;
    
    private void Awake()
    {       
        rigid = Util.GetOrAddComponent<Rigidbody>(gameObject);
        skillData = new SkillInfo(Managers.Data.SkillDict["ProjectileSkill"]);        
    }

    public override void Execute()
    {
        DefaultShoot();
        base.Execute();
    }

    private void DefaultShoot()
    {
        transform.position = new Vector3(Managers.Game.GetPlayer().transform.position.x, 1f, Managers.Game.GetPlayer().transform.position.z);       
        transform.forward = GetDirection();
        rigid.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }

    public Vector3 GetDirection() 
    {
        Vector3 direction = (targetPos - Managers.Game.GetPlayer().transform.position).normalized;
        direction.y = 0f;

        return direction;
    }
}
