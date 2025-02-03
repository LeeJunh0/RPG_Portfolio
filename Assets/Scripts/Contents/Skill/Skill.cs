using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Execute(Skill _skill);
}

public class Skill : MonoBehaviour
{
    public SkillInfo skillData;
    public Vector3 targetPos;

    public List<GameObject> objects = new List<GameObject>();
    public MotifyInfo[]     motifies = new MotifyInfo[3];
    public GameObject       prefab;

    public Motify           initialize;
    public Motify           embodiment;
    public Motify           movement;

    public virtual void Execute() 
    {
        SetMotifies();
        StartCoroutine(DestroySkill());

        initialize.Execute(this);
        embodiment.Execute(this);
        movement.Execute(this);
    }

    public void SetMotifies()
    {
        foreach (MotifyInfo info in motifies)
        {
            if (info == null)
                continue;

            Motify motify = Managers.Skill.SetMotify(info.skillName);

            if (motify is InitializeMotify)
                SetInitializeMotify(motify);
            else if(motify is EmbodimentMotify)
                SetEmbodimentMotify(motify);
            else
                SetMovementMotify(motify);
        }

        if (initialize == null) { SetInitializeMotify(new InitializeMotify()); }
        if (embodiment == null) { SetEmbodimentMotify(new EmbodimentMotify()); }
        if (movement == null) { SetMovementMotify(new MoveMotify()); }
    }

    public void SetInitializeMotify(Motify motify)    { initialize = motify; }
    public void SetEmbodimentMotify(Motify motify)    { embodiment = motify; }
    public void SetMovementMotify(Motify motify)      { movement = motify; }

    protected IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    protected virtual IEnumerator OnDamaged()
    {
        int layer = 1 << (int)Define.ELayer.Monster;

        yield return new WaitForSeconds(0.5f);

        Collider[] monsters = Physics.OverlapSphere(targetPos, skillData.radius / 2, layer);
    }

    private void OnDestroy()
    {
        movement.StopRun();
        StopCoroutine(OnDamaged());
    }
}
