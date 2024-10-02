using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : EmbodimentMotify
{
    public Horizontal(ProjectileSkill refSkill) : base(refSkill) { }

    public override void Embodiment(Transform pos)
    {
        int half = projectiles.Count / 2;
        for (int i = -half; i <= half; i++)
        {
            projectiles[i].transform.position = new Vector3(pos.position.x, 1f, pos.position.z) + pos.right * (i * 3f);
            projectiles[i].transform.forward = pos.forward;
        }
    }

    public override void Execute()
    {
        base.Execute();


    }
}
