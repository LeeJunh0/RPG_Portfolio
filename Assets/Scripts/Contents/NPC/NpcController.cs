using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcController : MonoBehaviour
{
    public Define.ENpc npcType = Define.ENpc.Normal;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        NpcInit();
    }

    public virtual void OnTypeUI() { }
    private void NpcInit() 
    {
        switch (npcType)
        {
            case Define.ENpc.Giver:
                break;
            case Define.ENpc.Trader:
                break;
            case Define.ENpc.Normal:
                break;
        }
    }
}
