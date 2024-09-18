using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcController : BaseController
{
    public Define.ENpc npcType = Define.ENpc.Normal;
    public override void Init()
    {
        NpcInit();
    }

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
