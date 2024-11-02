using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager
{
    public SkillInventory playerInventory;

    public void GetSkillInventory() { playerInventory = Managers.Game.GetPlayer().GetComponent<SkillInventory>(); }

    public Motify SetMotify(string name)
    {
        switch (name)
        {
            case "IceElemental":
                return new IceElemental();
            case "WindElemental": 
                return new WindElemental();
            case "FireElemental":
                return new FireElemental();
            case "EmbodimentMotify":
                return new EmbodimentMotify();
            case "HorizontalDeployment":
                return new HorizontalDeployment();
            case "DefenceDeployment":
                break;
            case "MoveMotify":
                return new MoveMotify();
            case "CircleMove":
                return new CircleMove();
        }

        return null;
    }

    public GameObject SetIndicator(Define.EIndicator type)
    {
        string typeName = Enum.GetName(typeof(Define.EIndicator), type);
        return Managers.Resource.Instantiate(typeName);
    }

    // 스킬들 -> 공격 -> 모든 공격의 효과. 범위형 혹은 단일형 범위형은 무슨 모양범위인지.
    // 각 공격들의 공격까지의 시간, 범위, 

    
}
