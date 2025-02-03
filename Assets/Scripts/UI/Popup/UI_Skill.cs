using Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Skill : UIPopup
{
    enum GameObjects
    {
        ProjectileTab,
        ExplosionTab,
        Initialize_MotifyGround,
        Embodiment_MotifyGround,
        Movement_MotifyGround,
        MainSkill_Slot
    }

    private UI_MotifyGround[] grounds = new UI_MotifyGround[3];
    private int slotCount = 3;

    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.ProjectileTab).BindEvent(evt => 
        {
            Managers.Skill.curMainSkill = Define.ESkill.Projectile;
            SetSkill();
        });
        GetObject((int)GameObjects.ExplosionTab).BindEvent(evt => 
        {
            Managers.Skill.curMainSkill = Define.ESkill.AreaOfEffect;
            SetSkill();
        });

        SetMotifys();
        SetSkill();     
    }

    private void SetSkill()
    {
        GameObject go = GetObject((int)GameObjects.MainSkill_Slot);
        UI_SkillSlot slot = go.GetComponent<UI_SkillSlot>();

        switch (Managers.Skill.curMainSkill)
        {
            case Define.ESkill.Projectile:
                slot.SetInfo(Managers.Data.SkillDict["ProjectileSkill"]);
                break;
            case Define.ESkill.AreaOfEffect:
                slot.SetInfo(Managers.Data.SkillDict["ExplosionSkill"]);
                break;
        }
        SetGroundInfo();
    }

    private void SetMotifys()
    {
        SetInitializeSlot();
        SetEmbodimentSlot();
        SetMovementSlot();
    }

    private void SetInitializeSlot()
    {
        Transform parent = GetObject((int)GameObjects.Initialize_MotifyGround).transform; 
        UI_MotifyGround ground = parent.GetComponent<UI_MotifyGround>();
        grounds[0] = ground;

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[100 + i]); 
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }

    private void SetEmbodimentSlot()
    {
        Transform parent = GetObject((int)GameObjects.Embodiment_MotifyGround).transform;
        UI_MotifyGround ground = parent.GetComponent<UI_MotifyGround>();
        grounds[1] = ground;

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();            
            slot.SetInfo(Managers.Data.MotifyDict[150 + i]); 
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }
    
    private void SetMovementSlot()
    {
        Transform parent = GetObject((int)GameObjects.Movement_MotifyGround).transform;
        UI_MotifyGround ground = parent.GetComponent<UI_MotifyGround>();
        grounds[2] = ground;

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[200 + i]);
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }

    private void SetGroundInfo()
    {      
        SkillInventory skillInven = Managers.Game.GetPlayer().GetComponent<SkillInventory>();
        SkillInfo mainSkill = skillInven.skillMotifies.FirstOrDefault(x => x.Key.type == Managers.Skill.curMainSkill).Key;

        if (mainSkill == null) return;

        for(int i = 0; i < grounds.Length; i++)
        {
            if (skillInven.skillMotifies[mainSkill][i] == null)
            {
                grounds[i].DeSelect();
                continue;
            }

            for (int j = 0; j < slotCount; j++)
            {
                if (skillInven.skillMotifies[mainSkill][i].Equals(grounds[i].slots[j].motifyInfo) == false)
                    continue;

                grounds[i].CheckSlots(grounds[i].slots[j]);
            }
        }
        
    }
}
