using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonsterController
{
    public override void Init()
    {
        WorldObjectType = Define.EWorldObject.Monster;
        stat = GetComponent<Stat>();

        CreateMiniMapIcon();
    }

    protected override void CreateMiniMapIcon()
    {
        base.CreateMiniMapIcon();

        meshRenderer.material.color = Color.cyan;
    }

    protected override void UpDateSkill()
    {
        
    }

    public void OnHardAttack()
    {

    }

    public void OnGroundAttack()
    {
        Vector3 direction = lockTarget.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > 5f)
        {
            EState = Define.EState.Idle;
            return;
        }

        float angle = Vector3.Angle(transform.forward, direction.normalized);

        if(120f <= angle / 2)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
        }
        EState = Define.EState.Idle;
    }
}
