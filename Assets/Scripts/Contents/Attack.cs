using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Attack : MonoBehaviour
{
    protected virtual IEnumerator OnDamaged()
    {
        float radius = transform.GetChild(0).GetComponent<SphereCollider>().radius;
        int layer = 1 << (int)Define.ELayer.Monster;

        yield return new WaitForSeconds(0.5f);

        float curSec = 0f;
        while(true)
        {
            if (curSec > 1f)
                break;

            AreaDamage(radius, layer);
            yield return new WaitForSeconds(0.2f);
            curSec += 0.2f;
        }
    }

    protected void AreaDamage(float radius, int layer)
    {
        Collider[] monsters = Physics.OverlapSphere(transform.position, radius, layer);
        for (int i = 0; i < monsters.Length; i++)
        {
            Stat monster = monsters[i].GetComponent<Stat>();
            monster.OnDamaged(Managers.Game.GetPlayer().GetComponent<Stat>());
        }
    }

    protected void TargetDamage(float radius, int layer)
    {
        
    }
}
