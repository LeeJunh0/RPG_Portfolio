using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Define;

// �߻�ü ��ų���� �����θ�
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
