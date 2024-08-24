using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject lockTarget = null;

    [SerializeField]
    protected Define.EState curState = Define.EState.Idle;

    [SerializeField]
    protected Vector3 DestPos;

    public virtual Define.EState EState
    {
        get { return curState; }
        set
        {
            curState = value;
            Animator anim = GetComponent<Animator>();

            switch (curState)
            {
                case Define.EState.Die:
                    break;
                case Define.EState.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.EState.Move:
                    anim.CrossFade("MOVE", 0.1f);
                    break;
                case Define.EState.Skill:
                    if(lockTarget.layer == (int)Define.ELayer.Monster)
                    {
                        anim.CrossFade("ATTACK", 0.1f, -1, 0);
                        break;
                    }
                    curState = Define.EState.Idle;
                    break;
            }
        }
    }

    public Define.EWorldObject WorldObjectType { get; protected set; } = Define.EWorldObject.Unknown;
    public abstract void Init();

    private void Start()
    {
        Init();
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

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateDie() { }
    protected virtual void UpDateSkill() { }
}
