using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class MoveMotify : Motify
{
    protected float speed = 20f;
    public MoveMotify(ProjectileSkill refSkill) : base(refSkill) { }

    public void Shooting()
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            Rigidbody rigid = projectiles[i].GetComponent<Rigidbody>();
            rigid.AddForce(projectiles[i].transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public virtual void Movement()
    {
        Shooting();
    }

    public override void Execute()
    {
        base.Execute();

        Movement();
    }

    public override void SetMana() { skill.skillData.Mana += 10; }
}
