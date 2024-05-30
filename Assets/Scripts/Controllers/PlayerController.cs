using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Speed = 10.0f;
    Vector3 DestPos;

    Animator anim;
    float Wait_Run_Ratio = 0.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;  
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Die,
    }

    PlayerState curState = PlayerState.Idle;

    void UpdateMove()
    {
        Vector3 dir = DestPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            curState = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent agent = gameObject.GetOrAddComponent<NavMeshAgent>();

            float Movedis = Mathf.Clamp(Speed * Time.deltaTime, 0f, dir.magnitude);
            agent.Move(dir.normalized * Movedis);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                curState = PlayerState.Idle;
                return;
            }
            

            //transform.position += dir.normalized * Movedis;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }

        // 애니메이션
        Wait_Run_Ratio = Mathf.Lerp(Wait_Run_Ratio, 1, 10 * Time.deltaTime);
        anim.SetFloat("Wait_Run_Ratio", Wait_Run_Ratio);
        anim.Play("Wait_Run");
    }

    void UpdateIdle()
    {
        // 애니메이션
        Wait_Run_Ratio = Mathf.Lerp(Wait_Run_Ratio, 0, 10 * Time.deltaTime);
        anim.SetFloat("Wait_Run_Ratio", Wait_Run_Ratio);
        anim.Play("Wait_Run");
    }

    void UpdateDie()
    {

    }

    void Update()
    {
        switch (curState)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.green, 1.0f);

        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100.0f,LayerMask.GetMask("Wall")))
        {
            DestPos = hit.point;
            curState = PlayerState.Move;
        }
    }
}
