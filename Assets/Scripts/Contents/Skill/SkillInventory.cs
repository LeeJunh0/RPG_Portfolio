using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    public List<Skill> skillInven;
    public List<Skill> mySkills;

    private void Start()
    {
        mySkills.Add(skillInven[0]);    
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(BindKey.SkillSlot_1))        
            StartCoroutine(SkillActivation(mySkills[0]));
        if (Input.GetKeyDown(BindKey.SkillSlot_2))
            StartCoroutine(SkillActivation(mySkills[1])); 
        if (Input.GetKeyDown(BindKey.SkillSlot_3))
            StartCoroutine(SkillActivation(mySkills[2]));
    }

    public void SetSkill()
    {
        BaseController owner = GetComponent<BaseController>();

        foreach(SkillInfo skill in Managers.Data.SkillDict.Values)
        {
            //if (skill.useObject == owner.WorldObjectType)
            //    skillInven.Add();
        }
    }

    public void AddSkill(Skill skill) { mySkills.Add(skill); }
    public void RemoveSkill(Skill skill) { mySkills.Remove(skill); }

    protected IEnumerator SkillActivation(Skill skill)
    {
        if (skill == null) 
            yield break;
        //if (skill.skillData.isActive == false) // ½ºÅ³ ÄðÅ¸ÀÓ
        //    yield break;

        GameObject skillPrefab = Managers.Resource.Instantiate(skill.gameObject.name);
        skillPrefab.transform.position = new Vector3(Managers.Game.GetPlayer().transform.position.x, 1f, Managers.Game.GetPlayer().transform.position.z);
        skillPrefab.transform.forward = transform.forward;

        Skill instanceSkill = skillPrefab.GetComponent<Skill>();
        instanceSkill.Execute();
        instanceSkill.skillData.isActive = false;

        yield return new WaitForSeconds(skill.skillData.coolTime);

        instanceSkill.skillData.isActive = true;
    }
}
