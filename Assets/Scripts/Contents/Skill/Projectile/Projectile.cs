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
        if (muzzleVFX == null) return;

        GameObject muzzlePrefab = Managers.Resource.Instantiate(muzzleVFX);
        muzzlePrefab.transform.position = Managers.Game.GetPlayer().transform.position;

        var particle = muzzlePrefab.GetComponent<ParticleSystem>();
        if (particle == null)
        {
            var child = muzzlePrefab.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(muzzlePrefab, child.main.duration);
        }
        else
            Destroy(muzzlePrefab, particle.main.duration);
    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.layer != (int)Define.ELayer.Monster && co.gameObject.layer != (int)Define.ELayer.Block)
            return;

        if (hitVFX == null) return;

        GameObject hitPrefab = Managers.Resource.Instantiate(hitVFX);
        hitPrefab.transform.position = co.transform.position;
        var particle = hitPrefab.GetComponent<ParticleSystem>();
        if (particle == null)
        {
            var child = hitPrefab.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitPrefab, child.main.duration);
        }
        else
            Destroy(hitPrefab, particle.main.duration);

        StartCoroutine(ParticleDestroy(0f));
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
