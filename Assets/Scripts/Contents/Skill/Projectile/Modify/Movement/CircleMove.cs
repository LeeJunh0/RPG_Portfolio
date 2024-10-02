using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MoveMotify
{
    float angle;
    float radius = 2f;

    public CircleMove(ProjectileSkill refSkill) : base(refSkill) { }

    public override void Movement()
    {
        angle += speed * Time.deltaTime;

        for (int i = 0; i < projectiles.Count; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            projectiles[i].transform.position += new Vector3(x, 1f, z);
        }
    }

    public override void Execute()
    {
        base.Execute();

        Movement();
    }
}
