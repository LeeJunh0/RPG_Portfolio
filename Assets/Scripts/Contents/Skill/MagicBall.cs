using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{ 
    public GameObject           player;
    public GameObject           hitVFX;
    public GameObject           muzzleVFX;
    float                       curTime;
    float                       lifeTime;

    private void Start()
    {
        lifeTime = 1.5f;
    }

    private void OnEnable()
    {
        if (muzzleVFX == null)
            return;
        else
            muzzleVFX = Managers.Resource.Instantiate(muzzleVFX.name, transform);

        var particle = muzzleVFX.GetComponent<ParticleSystem>();
        if (particle == null)
        {
            var particleChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Skill.skillInstance.balls.Remove(this);
            Managers.Resource.Destroy(muzzleVFX, particleChild.main.duration);
        }
        else
            Managers.Resource.Destroy(muzzleVFX, particle.main.duration);
    }

    private void Update()
    {
        curTime += Time.deltaTime;

        if(lifeTime <= curTime)
        {
            Skill.skillInstance.balls.Remove(this);
            StartCoroutine(DestroyParticle(0.02f));          
            curTime = 0f;
        }
    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.layer != (int)Define.ELayer.Monster && co.gameObject.layer != (int)Define.ELayer.Block)
            return;

        hitVFX = Managers.Resource.Instantiate(hitVFX.name, transform);
        var particle = hitVFX.GetComponent<ParticleSystem>();

        if (particle == null)
        {
            var particleChild = hitVFX.transform.GetChild(0);
            var ps = particleChild.GetComponent<ParticleSystem>();
            Managers.Resource.Destroy(hitVFX, ps.main.duration);
        }
        else
            Managers.Resource.Destroy(hitVFX, particle.main.duration);

        StartCoroutine(DestroyParticle(0.02f));
    }

    public IEnumerator DestroyParticle(float waitTime)
    {
        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Skill.skillInstance.balls.Remove(this);
        Managers.Resource.Destroy(gameObject);
    }
}
