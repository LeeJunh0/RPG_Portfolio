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

    GameObject indicator;

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

    public IEnumerator OnHardAttack()
    {
        Vector3 lockPos = lockTarget.transform.position;

        float curSec = 0f;
        float distance = 999f;

        while (curSec <= 3f && indicator != null)
        {
            curSec += Time.deltaTime;
            float indicatorY = Mathf.Lerp(indicator.transform.localScale.y, 1f, curSec / 3f);
            indicator.transform.localScale = new Vector3(5f, indicatorY, 1f);
            distance = Vector3.Distance(lockPos, transform.position);
            yield return null;
        }

        while (distance >= 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, lockPos, agent.speed * Time.deltaTime);
            yield return null;
        }

        Stat targetStat = lockTarget.GetComponent<Stat>();
        targetStat.OnDamaged(stat);
    }

    public IEnumerator OnGroundAttack()
    {
        yield return null;
    }
    public void GroundAttack()
    {
        Vector3 direction = lockTarget.transform.position - transform.position;
        float distance = direction.magnitude;

        float angle = Vector3.Angle(transform.forward, direction.normalized);

        if(120f >= angle / 2 && distance <= 5f)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
        }
    }
        
    public void OnSkill() 
    {
        agent.isStopped = true; 

    }

    public void OffSkill()
    {
        agent.isStopped = false;
        EState = Define.EState.Idle;
        Managers.Resource.Destroy(indicator);
    }

    private IEnumerator SkillExecute()
    {
        Define.EState cur = Define.EState.Idle;

        while (true)
        {
            if (lockTarget != null && (EState != Define.EState.HardAttack || EState != Define.EState.GroundAttack))            
                curSec++;

            if (curSec > 5f)
            {
                if (cur == Define.EState.HardAttack)
                    cur = Define.EState.GroundAttack;
                else
                    cur = Define.EState.HardAttack;

                EState = cur;
                curSec = 0f; 

                switch (EState)
                {
                    case Define.EState.HardAttack:
                        indicator = Managers.Skill.SetIndicator(Define.EIndicator.EnemyBoxIndicator);
                        break;
                    case Define.EState.GroundAttack:
                        indicator = Managers.Skill.SetIndicator(Define.EIndicator.EnemyArcIndicator);
                        break;
                }
                indicator.transform.SetParent(transform, false);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
