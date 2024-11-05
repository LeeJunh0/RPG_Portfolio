using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    protected Stat stat;
    protected NavMeshAgent agent;

    [SerializeField]
    protected float scanRange = 10f;

    public override void Init()
    {
        WorldObjectType = Define.EWorldObject.Monster;
        stat = GetComponent<Stat>();
        agent = gameObject.GetOrAddComponent<NavMeshAgent>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        CreateMiniMapIcon();
    }

    protected override void CreateMiniMapIcon()
    {
        base.CreateMiniMapIcon();

        meshRenderer.material.color = Color.red;
    }

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float dis = (player.transform.position - gameObject.transform.position).magnitude;
        if(dis <= scanRange)
        {
            lockTarget = player;
            EState = Define.EState.Move;
            return;
        }
    }

    protected override void UpdateMove()
    {
        agent.SetDestination(transform.position);

        if (lockTarget != null)
        {
            DestPos = lockTarget.transform.position;
            float distance = (DestPos - transform.position).magnitude;

            if (distance <= 1.5f)
            {
                EState = Define.EState.Skill;
                return;
            }
        }

        Vector3 dir = DestPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            EState = Define.EState.Idle;
        }
        else
        {
            agent.SetDestination(DestPos);
            agent.speed = stat.Movespeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }
    }

    protected override void UpdateDie()
    {
        Debug.Log("Monster Die");
    }

    protected override void UpDateSkill()
    {
        if (lockTarget == null)
            return;

        Vector3 dir = lockTarget.transform.position - transform.position;
        Quaternion quat = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 10 * Time.deltaTime);
    }

    protected virtual void OnHitEvent()
    {
        if(lockTarget == null)
        {
            EState = Define.EState.Idle;
            return;
        }

        Stat targetStat = lockTarget.GetComponent<Stat>();
        targetStat.OnDamaged(stat);

        if (targetStat.Hp < 0)
        {
            EState = Define.EState.Idle;
            return;
        }

        float dis = (lockTarget.transform.position - transform.position).magnitude;
        if (dis <= 1.5f)
            EState = Define.EState.Skill;
        else
            EState = Define.EState.Move;

    }
}
