using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    int mask =  (1 << (int)Define.ELayer.Ground) | (1 << (int)Define.ELayer.NPC);

    PlayerStat  stat;

    public override void Init()
    {
        WorldObjectType = Define.EWorldObject.Player;
        stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
        CreateMiniMapIcon();
    }

    protected override void CreateMiniMapIcon()
    {
        base.CreateMiniMapIcon();

        meshRenderer.material.color = Color.black;   
    }

    protected override void UpdateMove()
    {
        if(lockTarget != null)
        {
            float distance = (DestPos - transform.position).magnitude;
            if (distance <= 1.5f)
            {
                if (lockTarget.layer == (int)Define.ELayer.NPC)
                {
                    EState = Define.EState.Idle;
                    lockTarget.GetOrAddComponent<QuestGiver>().OnTypeUI();
                }                                             
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

    protected override void UpDateDodge()
    {
        EState = Define.EState.Idle;
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
                break;
        }
    }
    
    void OnKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Z))        
            EState = Define.EState.Skill;                        

        if (Input.GetKeyDown(KeyCode.X))
            EState = Define.EState.Dodge;
    }

    void OnSkillEvent() // 애니메이션 이벤트에 넣은 메서드
    {
        stat.Mp -= 10;      
        EState = Define.EState.Idle;
    }

    void OnMouseEvent_IdelRun(Define.EMouseEvent evt)
    {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 200f, Color.green);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, mask);

        if (EventSystem.current.IsPointerOverGameObject() == true) // UI클릭시 이동X
            return;

        switch (evt)
        {
            case Define.EMouseEvent.PointerDown:
                if (raycastHit == true)
                {
                    switch (hit.collider.gameObject.layer)
                    {
                        case (int)Define.ELayer.NPC:
                            lockTarget = hit.collider.gameObject;                            
                            break;
                        default:
                            lockTarget = null;
                            break;
                    }
                    DestPos = hit.point; 
                    GameObject arrow = Managers.Resource.Instantiate("ClickMoveArrows");
                    arrow.transform.position = DestPos;
                    EState = Define.EState.Move;

                }
                break;
            case Define.EMouseEvent.Press:
                if (lockTarget == null && raycastHit)
                    DestPos = hit.point;
                break;
            case Define.EMouseEvent.PointerUp:

                break;
        }
    }

}
