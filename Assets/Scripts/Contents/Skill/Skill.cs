using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected SkillData     skillData;
    protected bool          isActive = true;

    // 스킬 발동메서드
    protected virtual void Activation()
    {
        SkillInitialize();
        SkillRealization();
    }

    // 스킬 구현메서드

    protected abstract void SkillInitialize();
    protected abstract void SkillRealization();
    protected IEnumerator SkillCoolDown()
    {
        isActive = false;

        yield return new WaitForSeconds(skillData.CoolTime);

        isActive = true;
    }
}
