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
    public List<GameObject> projectiles;
    public GameObject       projectile;
    public GameObject       hitVFX;
    public GameObject       muzzleVFX;
    public int              count;

    Rigidbody rigid;
    
    private void Start()
    { 
        projectiles = new List<GameObject>();
        rigid = GetComponent<Rigidbody>();
        skillData = new SkillInfo(Managers.Data.SkillDict["Projectile"]);

        rigid.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }

    public override void Execute()
    {
        if (initialize == null) { initialize = new InitializeMotify(this); }
        if (embodiment == null) { embodiment = new EmbodimentMotify(this); }
        if (movement == null) { movement = new MoveMotify(this); }

        this.initialize.Execute();
        this.embodiment.Execute();
        this.movement.Execute(); 
        StartCoroutine(DestroySkill());
    }

    IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(2f);
        movement?.StopRun();
        Destroy(this.gameObject);
    }
}
