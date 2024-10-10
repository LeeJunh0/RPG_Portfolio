using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Define;

// 발사체 스킬들의 상위부모
public class ProjectileSkill : Skill
{
    public EProjectile      type = EProjectile.None;

    public List<GameObject> projectiles;
    public GameObject       projectile;
    public GameObject       hitVFX;
    public GameObject       muzzleVFX;
    public int              count;

    private void Start()
    { 
        projectiles = new List<GameObject>();
        skillData = new SkillInfo(Managers.Data.SkillDict["Projectile"]);
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
        yield return new WaitForSeconds(1.5f);
        movement?.StopRun();
        Destroy(this.gameObject);
    }
}
