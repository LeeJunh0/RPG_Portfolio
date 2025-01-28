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
    public Dictionary<SkillInfo, List<MotifyInfo>> skillMotifies;
    public List<SkillInfo> skillInven;
    public List<SkillInfo> mySkills;
    
    private BaseController owner;

    private void Start()
    {
        skillMotifies = new Dictionary<SkillInfo, List<MotifyInfo>>();
        InitSkills();
    }

    private void Update()
    {
        if (Input.GetKeyDown(BindKey.SkillSlot_1))
            StartCoroutine(SkillActivation(mySkills[0]));
        if (Input.GetKeyDown(BindKey.SkillSlot_2))
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
                    skillMotifies.Add(skillinfo, new List<MotifyInfo>());
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

        if (skillMotifies[skill].Any(motify => motify.skillName == info.skillName) == true)
        {
            Debug.Log("중복된 파츠 입니다.");
            return;
        }

        MotifyInfo removeValue = skillMotifies[skill].FirstOrDefault(x => x.type == info.type);
        if (removeValue != null)
            RemoveMotify(skill, removeValue);

        skillMotifies[skill].Add(info);
    }

    public void RemoveMotify(SkillInfo skill, MotifyInfo info) { skillMotifies[skill].Remove(info); }

    private bool CoolTimeCheck(SkillInfo skill) { return skill.isActive; }

    private IEnumerator PlayerSetIndicator(SkillInfo skill)
    {
        GameObject prefab = Managers.Skill.SetIndicator(skill.indicator);
        Indicator indicator = prefab.GetComponent<Indicator>();
        indicator.SetInfo(skill.indicator, skill.length, skill.radius);

        if (skill.indicator == EIndicator.CircleIndicator)
        {
            GameObject range = Managers.Skill.SetIndicator(EIndicator.RangeIndicator);
            CircleIndicator circle = indicator.GetComponent<CircleIndicator>();
            circle.SetRange(range);
        }

        while (Input.GetKey(BindKey.SkillSlot_1) || Input.GetKey(BindKey.SkillSlot_2))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(indicator.transform.position).z; 
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            indicator.UpdatePosition(worldPos);

            yield return null;
        }

        SkillExecute(skill, indicator.transform.position);
        Managers.Resource.Destroy(indicator.gameObject);
    }

    public void SkillExecute(SkillInfo skill, Vector3 pos)
    {
        GameObject skillPrefab = new GameObject(name: "SkillObject");
        Managers.Resource.AddComponentByName(skill.name, skillPrefab);
        Skill instanceSkill = skillPrefab.GetComponent<Skill>();
        instanceSkill.motifies = skillMotifies[skill].ToArray();
        instanceSkill.targetPos = pos;
        instanceSkill.Execute();
    }

    private IEnumerator SkillActivation(SkillInfo skill)
    {
        if (CoolTimeCheck(skill) == false)
            yield break;

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