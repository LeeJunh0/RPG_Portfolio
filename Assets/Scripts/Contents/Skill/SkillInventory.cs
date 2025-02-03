using Data;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class SkillInventory : MonoBehaviour
{
    public Dictionary<SkillInfo, MotifyInfo[]> skillMotifies;
    public List<SkillInfo> skillInven;
    public List<SkillInfo> mySkills;
    
    private BaseController owner;
    private bool duplicateCheck = false;

    private void Start()
    {
        skillMotifies = new Dictionary<SkillInfo, MotifyInfo[]>();
        InitSkills();
    }

    private void Update()
    {
        if (Input.GetKeyDown(BindKey.SkillSlot_1) && duplicateCheck == false)
            StartCoroutine(SkillActivation(mySkills[0]));
        if (Input.GetKeyDown(BindKey.SkillSlot_2) && duplicateCheck == false)
            StartCoroutine(SkillActivation(mySkills[1]));
    }

    public void InitSkills()
    {
        owner = GetComponent<BaseController>();
        Stat ownerStat = owner.GetComponent<Stat>();

        foreach (SkillInfo skillinfo in Managers.Data.SkillDict.Values)
        {
            if (skillinfo.useObject == owner.WorldObjectType)
            {
                skillInven.Add(skillinfo);

                if (ownerStat.Level >= skillinfo.level)
                {
                    AddSkill(skillinfo);
                    skillMotifies.Add(skillinfo, new MotifyInfo[3]);
                }
            }
        }
    }

    public void AddSkill(SkillInfo skillInfo) { mySkills.Add(skillInfo); }
    public void RemoveSkill(SkillInfo skillInfo) { mySkills.Remove(skillInfo); }

    public void AddMotify(MotifyInfo info)
    {
        SkillInfo skill = skillMotifies.FirstOrDefault(x => x.Key.type == Managers.Skill.curMainSkill).Key;

        if (skill == null)
            return;

        if (Managers.Skill.MotifyNameEqule(skillMotifies[skill], info) == true)
        {
            RemoveByNameMotify(skill, info);
            Debug.Log("중복파츠입니다.");
            return;
        }

        if (Managers.Skill.MotifyTypeEqule(skillMotifies[skill], info) == true)       
            RemoveByTypeMotify(skill, info);

        switch (info.type)
        {
            case EMotifyType.Initialize:
                skillMotifies[skill][0] = info;
                break;
            case EMotifyType.Embodiment:
                skillMotifies[skill][1] = info;
                break;
            case EMotifyType.Movement:
                skillMotifies[skill][2] = info;
                break;
        }
    }

    public void RemoveByNameMotify(SkillInfo skill, MotifyInfo info) 
    {
        for (int i = 0; i < skillMotifies[skill].Length; i++)
        {
            if (skillMotifies[skill][i].NameEquals(info) == true)
            {
                skillMotifies[skill][i] = null;
                break;
            }  
        }
    }

    public void RemoveByTypeMotify(SkillInfo skill, MotifyInfo info)
    {
        for (int i = 0; i < skillMotifies[skill].Length; i++)
        {
            if (skillMotifies[skill][i].TypeEquals(info) == true)
            {
                skillMotifies[skill][i] = null;
                break;
            }
        }
    }

    private bool CoolTimeCheck(SkillInfo skill) { return skill.isActive; }

    private IEnumerator PlayerSetIndicator(SkillInfo skill)
    {
        float manaSum = SkillManaSum(skill);
        bool manaCheck = SkillManaCheck(manaSum);

        if (manaCheck == false)
            yield break;

        GameObject prefab = Managers.Skill.SetIndicator(skill.indicator);
        Indicator indicator = prefab.GetComponent<Indicator>();
        indicator.SetInfo(skill.indicator, skill.length, skill.radius);

        if (skill.indicator == EIndicator.CircleIndicator)
            RangeIndicatorSet(indicator);

        bool isCancel = false;
        while (Input.GetKey(BindKey.SkillSlot_1) || Input.GetKey(BindKey.SkillSlot_2))
        {
            if (Input.GetMouseButtonDown(1))
            { 
                isCancel = true;
                break;
            }
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(indicator.transform.position).z; 
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            indicator.UpdatePosition(worldPos);

            yield return null;
        }
        
        if (isCancel == false)        
            SkillExecute(skill, indicator.transform.position);

        Managers.Resource.Destroy(indicator.gameObject); 
        duplicateCheck = false;
    }

    public void RangeIndicatorSet(Indicator indicator)
    {
        GameObject range = Managers.Skill.SetIndicator(EIndicator.RangeIndicator);
        CircleIndicator circle = indicator.GetComponent<CircleIndicator>();
        circle.SetRange(range);
    }

    public void SkillExecute(SkillInfo skill, Vector3 pos)
    {
        GameObject skillPrefab = new GameObject(name: "SkillObject");
        Managers.Resource.AddComponentByName(skill.name, skillPrefab);
        Skill instanceSkill = skillPrefab.GetComponent<Skill>();
        instanceSkill.motifies = skillMotifies[skill].ToArray();
        instanceSkill.targetPos = pos;
        instanceSkill.Execute();

        float mana = SkillManaSum(skill);
        PlayerManaUse(mana);
    }

    public float SkillManaSum(SkillInfo skill)
    {
        float usingMana = skill.mana;
        for (int i = 0; i < skillMotifies[skill].Length; i++)
        {
            if (skillMotifies[skill][i] == null)
                continue;

            usingMana += skillMotifies[skill][i].mana;
        }

        return usingMana;
    }

    public bool SkillManaCheck(float mana)
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        if (playerStat.Mp < mana)
            return false;

        return true;
    }

    public void PlayerManaUse(float mana)
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.Mp -= mana;
    }

    private IEnumerator SkillActivation(SkillInfo skill)
    {
        if (CoolTimeCheck(skill) == false)
            yield break;

        duplicateCheck = true;
        StartCoroutine(PlayerSetIndicator(skill));
        
        skill.isActive = false;
        yield return new WaitForSeconds(skill.coolTime);
        skill.isActive = true;
    }

    private void OnDisable()
    {
        if (owner.WorldObjectType != EWorldObject.Monster)
            return;

        StopAllCoroutines();
    }
}