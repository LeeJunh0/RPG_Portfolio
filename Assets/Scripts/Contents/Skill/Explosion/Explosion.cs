using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Attack
{
    private void Start()
    {
        StartCoroutine(OnDamaged());   
    }

    private void OnDestroy()
    {
        StopCoroutine(OnDamaged());
    }
}
