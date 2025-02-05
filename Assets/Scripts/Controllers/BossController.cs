using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class BossController : MonsterController
{
    [SerializeField]
    private float curSec = 0f;

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
        OnSkill();
        Indicator indicator = IndicatorExecute(Define.EState.HardAttack);
        indicator.transform.parent = this.gameObject.transform;

        float sec = 0f;
        while(sec <= 5f)
        {
            sec += Time.deltaTime;
            indicator.UpdatePosition(transform.position);
            indicator.UpdateRotate((lockTarget.transform.position - transform.position).normalized, 10f);

            float lerpX = Mathf.Lerp(0.1f, 5f, sec / 3f);
            indicator.transform.localScale = new Vector3(lerpX, 1, 1);

            Vector3 direction = (lockTarget.transform.position - transform.position).normalized;
            Quaternion targetRotate = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, 10f * Time.deltaTime);

            yield return null;          
        }
        Managers.Resource.Destroy(indicator.gameObject);

        StartCoroutine(HardAttack());
    }

    private IEnumerator HardAttack()
    {
        EState = Define.EState.HardAttack;
        Vector3 prevPos = transform.position;
        Vector3 prevForward = transform.forward;
        float speed = stat.Movespeed * 1.5f;
        float sec = 0f;

        while (true)
        {
            sec += Time.deltaTime;
            float moveLength = Vector3.Distance(prevPos, transform.position);
            if (AttackChecking(prevPos, 2f) == true)
            {
                Stat targetStat = lockTarget.GetComponent<Stat>();
                targetStat.OnDamaged(stat);
                break;
            }
            else if(moveLength >= 40f || sec >= 5f)
                break;
                          
            transform.position += prevForward * Time.deltaTime * speed;
            yield return new WaitForFixedUpdate();
        }

        OffSkill();
    }
    private bool AttackChecking(Vector3 prevPos, float maxLength)
    {
        float distance = Vector3.Distance(lockTarget.transform.position, transform.position);
        return distance <= maxLength;
    }

    public IEnumerator OnGroundAttack()
    {
        OnSkill();
        Indicator indicator = IndicatorExecute(Define.EState.GroundAttack);
        indicator.InitRotate(lockTarget.transform.position - transform.position);
        indicator.transform.parent = this.gameObject.transform;

        float sec = 0f;
        while (sec <= 5f)
        {
            sec += Time.deltaTime;
            indicator.UpdatePosition(transform.position);
            float lerpX = Mathf.Lerp(0.1f, 3f, sec / 3f);
            indicator.transform.localScale = new Vector3(lerpX, lerpX * 1.5f, 1);

            if (sec <= 4)
                indicator.UpdateRotate(lockTarget.transform.position - transform.position, 20f);
            else if (sec >= 4f && EState != Define.EState.GroundAttack)
                EState = Define.EState.GroundAttack;

            yield return null;
        }
        
        Managers.Resource.Destroy(indicator.gameObject);
    }

    public void GroundAttack()
    {
        Vector3 direction = lockTarget.transform.position - transform.position;        
        float distance = direction.magnitude;
        float angle = Vector3.Angle(transform.forward, direction.normalized);

        if(120f >= angle / 2 && distance <= 6f)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
        }
    }
        
    public void OnSkill() 
    {
        agent.isStopped = true;
        agent.updateRotation = false;
        Animator anim = GetComponent<Animator>();
        anim.CrossFade("WAIT", 0.1f);
    }

    public void OffSkill()
    {
        agent.isStopped = false;
        agent.updateRotation = true;
        EState = Define.EState.Idle;
    }

    private Indicator IndicatorExecute(Define.EState type)
    {
        GameObject go = null;
        
        switch (type)
        {
            case Define.EState.HardAttack:
                go = Managers.Skill.SetIndicator(Define.EIndicator.EnemyBoxIndicator);
                break;
            case Define.EState.GroundAttack:
                go = Managers.Skill.SetIndicator(Define.EIndicator.EnemyArcIndicator);
                break;
        }

        Indicator indicator = go.GetComponent<Indicator>();
        return indicator;
    }

    private IEnumerator SkillExecute()
    {
        Define.EState cur = Define.EState.Idle;

        while (true)
        {
            if (lockTarget != null && (EState != Define.EState.HardAttack && EState != Define.EState.GroundAttack))            
                curSec++;

            if (curSec > 3f)
            {
                //cur = Define.EState.GroundAttack;
                cur = (cur == Define.EState.GroundAttack) ? Define.EState.HardAttack : Define.EState.GroundAttack;                  
                curSec = 0f; 

                switch (cur)
                {
                    case Define.EState.HardAttack:
                        yield return StartCoroutine(OnHardAttack());                       
                        break;
                    case Define.EState.GroundAttack:                       
                        yield return StartCoroutine(OnGroundAttack());
                        break;
                }                
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
