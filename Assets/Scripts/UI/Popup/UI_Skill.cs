using System.Collections;
using System.Collections.Generic;
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

    int slotCount = 3;
    Define.ESkill curSkillType;

    public Define.ESkill SkillType { get { return curSkillType; } set { curSkillType = value; } }
    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));

        SkillType = Define.ESkill.Projectile;

        SetSkill();
        SetMotifys();
    }

    private void SetSkill()
    {
        GameObject go = GetObject((int)GameObjects.MainSkill_Slot);
        UI_SkillSlot slot = go.GetComponent<UI_SkillSlot>();

        switch (SkillType)
        {
            case Define.ESkill.Projectile:
                slot.SetInfo(Managers.Data.SkillDict["Projectile"]);
                break;
            case Define.ESkill.AreaOfEffect:
                slot.SetInfo(Managers.Data.SkillDict["Explosion"]);
                break;
        }
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

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[200 + i]);
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }

    private void TabClickEvent()
    {
        
    }
}
