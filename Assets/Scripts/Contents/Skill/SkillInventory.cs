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
    public List<SkillInfo> skillInven;
    public List<SkillInfo> mySkills;
    public Dictionary<SkillInfo, List<MotifyInfo>> skillMotifies;

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
        BaseController owner = GetComponent<BaseController>();
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

    public void AddSkill(SkillInfo skillinfo) { mySkills.Add(skillinfo); }
    public void RemoveSkill(SkillInfo skillinfo) { mySkills.Remove(skillinfo); }

    public void AddMotify(MotifyInfo info)
    {
        // info에 맞는 타입의 스킬이 있는지
        SkillInfo skill = skillMotifies.FirstOrDefault(x => x.Key.type == info.owner).Key;

        if (skill == null)  
            return; 
        
        // 이름으로 중복확인
        if (skillMotifies[skill].Any(motify => motify.skillName == info.skillName) == true)
        {
            Debug.Log("중복된 스킬은 적용되지 않습니다..");
            return;
        }

        // 현재 넣어놓은 motify중 info와 같은 타입이 있는지 확인 하고 있던거 삭제 후 새로 삽입 
        MotifyInfo removeValue = skillMotifies[skill].FirstOrDefault(x => x.type == info.type);
        if (removeValue != null)
            RemoveMotify(skill, removeValue);

        skillMotifies[skill].Add(info);
    }

    public void RemoveMotify(SkillInfo skill, MotifyInfo info) { skillMotifies[skill].Remove(info); }
    private bool CoolTimeCheck(SkillInfo skill) { return skill.isActive; }

    private IEnumerator SetIndicator(SkillInfo skill)
    {
        GameObject prefab = Managers.Skill.SetIndicator(skill.indicator);        
        Indicator indicator = prefab.GetComponent<Indicator>();
        indicator.SetInfo(skill.indicator, skill.length, skill.radius);

        if(skill.indicator == EIndicator.CircleIndicator)
        {
            GameObject range = Managers.Skill.SetIndicator(EIndicator.RangeIndicator);
            CircleIndicator circle = indicator.GetComponent<CircleIndicator>();
            circle.SetRange(range);
        }

        while(Input.GetKey(BindKey.SkillSlot_1) || Input.GetKey(BindKey.SkillSlot_2))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(indicator.transform.position).z; // 현재 오브젝트의 깊이에 맞춤
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            indicator.UpdatePosition(worldPos);

            yield return null;
        }
        
        PlyaerSkillExecute(skill, indicator.transform.position);
        Managers.Resource.Destroy(indicator.gameObject);
    }

    public void PlyaerSkillExecute(SkillInfo skill, Vector3 pos)
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

        switch (skill.useObject)
        {
            case EWorldObject.Player:
                StartCoroutine(SetIndicator(skill));
                break;
            case EWorldObject.Monster:
                break;
            default:
                break;
        }
        
        skill.isActive = false;
        yield return new WaitForSeconds(skill.coolTime);
        skill.isActive = true;
    }
}
