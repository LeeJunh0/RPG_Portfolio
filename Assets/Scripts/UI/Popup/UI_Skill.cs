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

    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));

        SetMotifys();
    }

    private void SetMotifys()
    {
        SetInitializeSlot();
        SetEmbodimentSlot();
        SetMovementSlot();
    }

    private void SetInitializeSlot()
    {
        for (int i = 1; i <= slotCount; i++)
        {
            Transform parent = GetObject((int)GameObjects.Initialize_MotifyGround).transform;

            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[100 + i]); 
            prefab.transform.SetParent(parent);
        }
    }

    private void SetEmbodimentSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Transform parent = GetObject((int)GameObjects.Embodiment_MotifyGround).transform;

            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[150 + i]); 
            prefab.transform.SetParent(parent);
        }
    }

    private void SetMovementSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Transform parent = GetObject((int)GameObjects.Movement_MotifyGround).transform;

            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[200 + i]);
            prefab.transform.SetParent(parent);
        }
    }

    private void TabClickEvent()
    {
        
    }
}
