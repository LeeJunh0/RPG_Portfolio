using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class BossController : MonsterController
{
    [SerializeField]
    float curSec = 0f;

    public override Define.EState EState
    {
        get { return curState; }
        set
        {           
            switch (curState)
            {
                case Define.EState.HardAttack:
                    break;
                case Define.EState.GroundAttack:
                    break;
            }

            base.EState = value;
        }
    }

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
        Indicator indicator = IndicatorExecute(Define.EState.HardAttack);

        curSec = 0f;
        float distance = 0;

        while(curSec <= 3f)
        {
            curSec += Time.deltaTime;
            indicator.UpdatePosition(transform.position);
            indicator.UpdateRotate((lockTarget.transform.position - transform.position).normalized);

            float lerpX = Mathf.Lerp(0.1f, 5f, curSec / 3f);
            indicator.transform.localScale = new Vector3(lerpX, 1, 1);
            yield return null;
        }

        Stat targetStat = lockTarget.GetComponent<Stat>();
        targetStat.OnDamaged(stat); 
        OffSkill(indicator.gameObject);
    }

    public IEnumerator OnGroundAttack()
    {
        OnSkill();
        Indicator indicator = IndicatorExecute(Define.EState.GroundAttack);

        curSec = 0f;
        while (curSec >= 3f)
        {
            curSec += Time.deltaTime;
            indicator.UpdatePosition(transform.position);
            indicator.UpdateRotate(lockTarget.transform.position - transform.position);

            yield return null;
        }

        Stat targetStat = lockTarget.GetComponent<Stat>();
        targetStat.OnDamaged(stat);
        OffSkill(indicator.gameObject);
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
    public void OffSkill(GameObject go)
    {
        agent.isStopped = false;
        EState = Define.EState.Idle;
        Managers.Resource.Destroy(go);
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

            if (curSec > Random.Range(3f, 6f))
            {
                cur = Define.EState.HardAttack;
                //cur = (cur == Define.EState.GroundAttack) ? Define.EState.HardAttack : Define.EState.GroundAttack;                  
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
