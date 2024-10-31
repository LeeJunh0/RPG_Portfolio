using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CircleMove : MoveMotify
{
    float angle;
    float radius = 1.5f;

    public override IEnumerator Movement()
    {
        angle = 0f;
        speed = 300f;
        Debug.Log($"{GetType().Name} : 코루틴 실행중");
        while (true) 
        {
            yield return new WaitForFixedUpdate();
            angle += speed * Time.deltaTime;

            if (angle >= 360)
                angle = 0f;

            for (int i = 0; i < objects.Count; i++)
            {
                float radian = Mathf.Deg2Rad * (angle + (360 / objects.Count) * i);
                float x = Mathf.Cos(radian) * radius;
                float z = Mathf.Sin(radian) * radius;

                if(objects[i] != null)
                    objects[i].transform.position = new Vector3(skill.transform.position.x + x, 1f, skill.transform.position.z + z);
            }
        }          
    }

    public override void Execute(Skill _skill)
    {
        base.Execute(_skill);
        
        CoroutineRunner.Instance.RunCoroutine(Movement());
    }
}
