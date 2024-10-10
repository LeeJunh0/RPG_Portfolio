using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MoveMotify
{
    float angle;
    float radius = 2f;

    public CircleMove(ProjectileSkill refSkill) : base(refSkill) { }

    public override IEnumerator Movement()
    {
        yield return new WaitForFixedUpdate();
        angle += speed * Time.deltaTime;

        for (int i = 0; i < projectiles.Count; i++)
        {
            float radian = Mathf.Deg2Rad * (angle + (i * (360 / projectiles.Count)));
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            projectiles[i].transform.position = new Vector3(x, 1f, z);
        }
    }

    public override void Execute()
    {
        CoroutineRunner.Instance.RunCoroutine(Movement());
    }
}
