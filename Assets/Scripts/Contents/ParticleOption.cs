using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOption : MonoBehaviour
{
    float lifetime;

    private void OnEnable()
    {
        lifetime = 0f;
    }

    private void Update()
    {
        if(lifetime >= 2f)
            Managers.Resource.Destroy(gameObject);

        lifetime += Time.deltaTime;
    }

}
