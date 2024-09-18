using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat stat;

    [SerializeField]
    float scanRange = 10f;

    public override void Init()
    {
        WorldObjectType = Define.EWorldObject.Monster;
        stat = GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        CreateMiniMapIcon();
    }

    private void Update()
    {
        minimapIcon.transform.position = new Vector3(   transform.position.x, 
                                                        minimapIcon.transform.position.y, 
                                                        transform.position.z);
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
        if (lockTarget != null)
        {
            DestPos = lockTarget.transform.position;
            float distance = (DestPos - transform.position).magnitude;

            if (distance <= attackRange)
            {
                NavMeshAgent agent = gameObject.GetOrAddComponent<NavMeshAgent>();
                agent.SetDestination(transform.position);

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
            NavMeshAgent agent = gameObject.GetOrAddComponent<NavMeshAgent>();
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
        if (lockTarget != null)
        {
            Vector3 dir = lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (lockTarget != null)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);

            if(targetStat.Hp > 0)
            {
                float dis = (lockTarget.transform.position - transform.position).magnitude;
                if (dis <= attackRange)
                    EState = Define.EState.Skill;
                else
                    EState = Define.EState.Move;
            }
            else
            {
                EState = Define.EState.Idle;
            }
        }
        else
        {
            EState = Define.EState.Idle;
        }
    }
}
