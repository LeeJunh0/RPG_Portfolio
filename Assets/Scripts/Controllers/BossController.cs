using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonsterController
{
    [SerializeField]
    float curSec = 0f;

    public override void Init()
    {
        base.Init();

        StartCoroutine(SkillExecute());
    }
    protected override void CreateMiniMapIcon()
    {
        base.CreateMiniMapIcon();

        meshRenderer.material.color = Color.cyan;
    }

    public void OnHardAttack()
    {
        Vector3 lockPos = lockTarget.transform.position;
        float distance = Vector3.Distance(lockPos, transform.position);

        if(distance <= 2f)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
        }
    }

    public void OnGroundAttack()
    {
        Vector3 direction = lockTarget.transform.position - transform.position;
        float distance = direction.magnitude;

        float angle = Vector3.Angle(transform.forward, direction.normalized);

        if(120f <= angle / 2)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
        }
    }
        
    public void OnSkill() 
    {
        agent.isStopped = true;
    }

    public void OffEState()
    {
        agent.isStopped = false;
        agent.speed = stat.Movespeed;

        EState = Define.EState.Idle;
    }

    private IEnumerator SkillExecute()
    {
        Define.EState cur = Define.EState.HardAttack;

        while (true)
        {
            if (lockTarget != null)            
                curSec++;

            if (curSec > 5f)
            {
                if (cur == Define.EState.HardAttack)
                    cur = Define.EState.GroundAttack;
                else
                    cur = Define.EState.HardAttack;

                EState = cur;
                curSec = 0f;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
