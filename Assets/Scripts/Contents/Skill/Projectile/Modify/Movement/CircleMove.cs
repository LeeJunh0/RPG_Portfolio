using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CircleMove : MoveMotify
{
    float angle;
    float radius = 1.5f;

    public CircleMove(ProjectileSkill refSkill) : base(refSkill) { }

    public override IEnumerator Movement()
    {
        speed = 300f;

        while (true) 
        {
            yield return new WaitForFixedUpdate();
            angle += speed * Time.deltaTime;

            for (int i = 0; i < projectiles.Count; i++)
            {
                float radian = Mathf.Deg2Rad * (angle + (360 / projectiles.Count) * i);
                float x = Mathf.Cos(radian) * radius;
                float z = Mathf.Sin(radian) * radius;

                if(projectiles[i] != null)
                    projectiles[i].transform.position = new Vector3(skill.transform.position.x + x, 1f, skill.transform.position.z + z);
            }
        }    
    }

    public override void Execute()
    {
        CoroutineRunner.Instance.RunCoroutine(Movement());
    }
}
