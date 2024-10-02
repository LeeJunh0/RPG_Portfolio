using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbodimentMotify : Motify
{
    public EmbodimentMotify(ProjectileSkill refSkill) : base(refSkill) { }

    public virtual void Embodiment(Transform pos) 
    {
        for(int i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].transform.position = new Vector3(pos.position.x, 1f, pos.position.z) + pos.forward;
            projectiles[i].transform.forward = pos.forward;
        }
    }

    public override void SetMana() { }

    public override void Execute()
    {
        base.Execute();

        Embodiment(Managers.Game.GetPlayer().transform);
    }
}
