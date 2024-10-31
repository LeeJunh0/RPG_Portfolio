using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject(name: "CoroutineRunner");
                go.AddComponent<CoroutineRunner>(); 
                instance = go.GetComponent<CoroutineRunner>();
                DontDestroyOnLoad(go);
            }            
            return instance;
        }
    }

    public Coroutine RunCoroutine(IEnumerator coroutine) { return StartCoroutine(coroutine); }
    public void StopRunCoroutine(IEnumerator coroutine, string name)
    {
        if (coroutine == null)
        {
            Debug.Log("���������� �ڷ�ƾ �����䱸");
            return;
        }

        Debug.Log($"{name} : �ڷ�ƾ ����");
        StopCoroutine(coroutine);
    }
}
