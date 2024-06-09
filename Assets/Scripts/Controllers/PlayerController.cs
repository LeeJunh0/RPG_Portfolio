using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat stat;
    bool stopSkill = false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
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
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = DestPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;
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
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
        
    }

    void Update()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Move:
                UpdateMove();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Skill:
                UpDateSkill();
                break;
        }
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdelRun(evt);
                break;
            case Define.State.Move:
                OnMouseEvent_IdelRun(evt);
                break;
            case Define.State.Die:
                break;
            case Define.State.Skill:
                if (evt == Define.MouseEvent.PointerUp)
                    stopSkill = true;
                break;
        }
    }

    void OnMouseEvent_IdelRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                if (raycastHit == true)
                {
                    DestPos = hit.point;
                    State = Define.State.Move;
                    stopSkill = false;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        lockTarget = hit.collider.gameObject;
                    else
                        lockTarget = null;
                }
                break;
            case Define.MouseEvent.Press:
                if (lockTarget == null && raycastHit)
                    DestPos = hit.point;
                break;
            case Define.MouseEvent.PointerUp:
                stopSkill = true;
                break;
        }
    }
}
