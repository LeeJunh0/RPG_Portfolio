using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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


    public ProjectileSkill() { projectiles = new List<GameObject>(); }


    public override void Execute() { base.Execute(); }
}
