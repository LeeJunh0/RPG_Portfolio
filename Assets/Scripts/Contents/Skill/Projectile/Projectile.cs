using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Projectile : MonoBehaviour
{
    public GameObject hitVFX;
    public GameObject muzzleVFX;

    void Start()
    {
        if(muzzleVFX == null)
        {
            Debug.Log("Faild Load muzzleVFX");
            return;
        }

        var particle = muzzleVFX.GetComponent<ParticleSystem>();
        if (particle == null)
        {
            var child = particle.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            StartCoroutine(DurationDestroy(muzzleVFX, child.main.duration));
        }
        else
            StartCoroutine(DurationDestroy(muzzleVFX, particle.main.duration));
    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.layer != (int)Define.ELayer.Monster && co.gameObject.layer != (int)Define.ELayer.Block)
            return;

        if (hitVFX == null)
        {
            Debug.Log("Faild Load hitVFX");
            return;
        }

        var particle = hitVFX.GetComponent<ParticleSystem>();
        if (particle == null)
        {
            var child = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitVFX, child.main.duration);
        }
        else
            Destroy(hitVFX, particle.main.duration);

        StartCoroutine(ParticleDestroy(0.2f));
    }

    IEnumerator DurationDestroy(GameObject go, float sec)
    {
        yield return new WaitForSeconds(sec);
        Managers.Resource.Destroy(go);
    }

    IEnumerator ParticleDestroy(float sec)
    {
        if (transform.childCount > 0 && sec != 0)
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
        yield return new WaitForSeconds(sec);
        Managers.Resource.Destroy(this.gameObject);
    }
}
