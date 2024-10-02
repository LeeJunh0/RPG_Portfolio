using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    public Skill[] skills;

    private void Start()
    {
        skills = new Skill[3];
    }

    private void Update()
    {
        if (Input.GetKeyDown(BindKey.SkillSlot_1))
        {
            skills[0].Execute(); 
        }
    }

    public void SkillCheck()
    {

    }

    protected IEnumerator SkillCoolDown(int index)
    {
        skills[index].skillData.IsActive = false;

        yield return new WaitForSeconds(skills[index].skillData.CoolTime);

        skills[index].skillData.IsActive = true;
    }

}
