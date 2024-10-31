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
    }

    public void SetMotifies()
    {
        foreach(MotifyInfo info in motifies)
        {
            Motify motify = Managers.Skill.SetMotify(info.skillName);

            if(motify is InitializeMotify)
                SetInitializeMotify(motify);
            else if(motify is EmbodimentMotify)
                SetEmbodimentMotify(motify);
            else
                SetMovementMotify(motify);
        }
    }

    public void SetInitializeMotify(Motify motify)    { initialize = motify; }
    public void SetEmbodimentMotify(Motify motify)    { embodiment = motify; }
    public void SetMovementMotify(Motify motify)      { movement = motify; }
    protected IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        movement.StopRun();
    }
}
