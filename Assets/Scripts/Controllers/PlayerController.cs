using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int mask = (1 << (int)Define.ELayer.Ground) | (1 << (int)Define.ELayer.Monster);

    PlayerStat stat;
    bool stopSkill = false;

    public override void Init()
    {
        WorldObjectType = Define.EWorldObject.Player;
        stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateMove()
    {
        if(lockTarget != null)
        {
            float distance = (DestPos - transform.position).magnitude;

            if(distance <= 1.5f)
            {
                EState = Define.EState.Skill;
                return;
            }
        }

        Vector3 dir = DestPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f)
        {
            EState = Define.EState.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    EState = Define.EState.Idle;
                return;
            }
            float Movedis = Mathf.Clamp(stat.Movespeed * Time.deltaTime, 0f, dir.magnitude);
            transform.position += dir.normalized * Movedis;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }

    }

    protected override void UpdateIdle()
    {

    }

    protected override void UpdateDie()
    {

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
        if(lockTarget != null)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
        }

        if(stopSkill == true)
        {
            EState = Define.EState.Idle;
        }
        else
        {
            EState = Define.EState.Skill;
        }
        
    }

    void Update()
    {
        switch (EState)
        {
            case Define.EState.Idle:
                UpdateIdle();
                break;
            case Define.EState.Move:
                UpdateMove();
                break;
            case Define.EState.Die:
                UpdateDie();
                break;
            case Define.EState.Skill:
                UpDateSkill();
                break;
        }
    }

    void OnMouseEvent(Define.EMouseEvent evt)
    {
        switch (EState)
        {
            case Define.EState.Idle:
                OnMouseEvent_IdelRun(evt);
                break;
            case Define.EState.Move:
                OnMouseEvent_IdelRun(evt);
                break;
            case Define.EState.Die:
                break;
            case Define.EState.Skill:
                if (evt == Define.EMouseEvent.PointerUp)
                    stopSkill = true;
                break;
        }
    }

    void OnMouseEvent_IdelRun(Define.EMouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, mask);

        switch (evt)
        {
            case Define.EMouseEvent.PointerDown:
                if (raycastHit == true)
                {
                    DestPos = hit.point;
                    EState = Define.EState.Move;
                    stopSkill = false;

                    if (hit.collider.gameObject.layer == (int)Define.ELayer.Monster)
                        lockTarget = hit.collider.gameObject;
                    else
                        lockTarget = null;
                }
                break;
            case Define.EMouseEvent.Press:
                if (lockTarget == null && raycastHit)
                    DestPos = hit.point;
                break;
            case Define.EMouseEvent.PointerUp:
                stopSkill = true;
                break;
        }
    }
}
