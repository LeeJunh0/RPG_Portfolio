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
    public MoveMotify(ProjectileSkill refSkill) : base(refSkill) { }

    public virtual IEnumerator Movement()
    {
        yield return null;
        for (int i = 0; i < projectiles.Count; i++)
        {
            Rigidbody rigid = projectiles[i].GetComponent<Rigidbody>();
            rigid.AddForce(projectiles[i].transform.forward * speed, ForceMode.Impulse);
        }
    }

   
    public override void Execute()  { CoroutineRunner.Instance.StartCoroutine(Movement()); }
    public void StopRun() { CoroutineRunner.Instance.StopCoroutine(Movement()); }
    public override void SetMana()  { skill.skillData.mana += 10; }
}
