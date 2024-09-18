using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickArrow : MonoBehaviour
{
    float lifeTime = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime >= 2f)
        {
            lifeTime = 0f;
            Managers.Resource.Destroy(gameObject);
        }         
    }
}
