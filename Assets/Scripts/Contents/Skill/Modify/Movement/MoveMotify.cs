using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class MoveMotify : Motify
{
    protected float speed = 40f;

    public virtual IEnumerator Movement()
    {
        Debug.Log($"{GetType().Name} : 코루틴 실행중");
        yield return null;
        for (int i = 0; i < objects.Count; i++)
        {
            Rigidbody rigid = objects[i].GetComponent<Rigidbody>();
            rigid.AddForce(objects[i].transform.forward * speed, ForceMode.Impulse);
        }
    }

    public override void Execute(Skill _skill) 
    { 
        base.Execute(_skill);
        
        if(skill.skillData.type == ESkill.Projectile)
            CoroutineRunner.Instance.StartCoroutine(Movement());
    }

    public override void StopRun() { CoroutineRunner.Instance.StopRunCoroutine(Movement(), GetType().Name); }
    public override void SetMana()  { skill.skillData.mana += 10; }
}
