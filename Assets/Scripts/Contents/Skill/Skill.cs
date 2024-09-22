using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected SkillData     skillData;
    protected bool          isActive = true;

    // ��ų �ߵ��޼���
    protected virtual void Activation()
    {
        SkillInitialize();
        SkillRealization();
    }

    // ��ų �����޼���

    protected abstract void SkillInitialize();
    protected abstract void SkillRealization();
    protected IEnumerator SkillCoolDown()
    {
        isActive = false;

        yield return new WaitForSeconds(skillData.CoolTime);

        isActive = true;
    }
}
