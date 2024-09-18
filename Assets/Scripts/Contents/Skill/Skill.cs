using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public static Skill skillInstance;
    public GameObject prefabMagic;
    public int count;
    public float speed;

    private void Awake()
    {
        skillInstance = this;
    }
}
