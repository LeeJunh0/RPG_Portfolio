using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject lockTarget = null;

    [SerializeField]
    protected Define.State curState = Define.State.Idle;

    [SerializeField]
    protected Vector3 DestPos;

    public virtual Define.State State
    {
        get { return curState; }
        set
        {
            curState = value;
            Animator anim = GetComponent<Animator>();

            switch (curState)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Move:
                    anim.CrossFade("MOVE", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;
    public abstract void Init();

    private void Start()
    {
        Init();
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

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateDie() { }
    protected virtual void UpDateSkill() { }
}
