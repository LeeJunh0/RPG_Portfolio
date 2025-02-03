using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager
{
    public SkillInventory playerInventory;
    public Define.ESkill curMainSkill = Define.ESkill.Projectile;
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

    public bool MotifyNameEqule(MotifyInfo[] infos, MotifyInfo info)
    {
        for(int i = 0; i < infos.Length; i++)
        {
            if (infos[i] == null) continue;

            if (infos[i].skillName == info.skillName)
                return true;
        }

        return false;
    }
    public bool MotifyTypeEqule(MotifyInfo[] infos, MotifyInfo info)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i] == null) continue;

            if (infos[i].TypeEquals(info) == true)
                return true;
        }

        return false;
    }
}
