using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public class ResourceManager
{
    Dictionary<string, Object> resourceDic = new Dictionary<string, Object>();
    Dictionary<string, AsyncOperationHandle> handleDic = new Dictionary<string, AsyncOperationHandle>();

    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            
            if(index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load Prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }
    public void Destroy(GameObject Go)
    {
        if (Go == null)
            return;

        Poolable poolable = Go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(Go);
    }

    #region Addressable
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
    {
        if(resourceDic.TryGetValue(key, out Object resource))
        {
            callback.Invoke(resource as T);
            return;
        }

        var asyncOperation = Addressables.LoadAssetAsync<T>(key);
        asyncOperation.Completed += (op) =>
        {
            resourceDic.Add(key, op.Result);
            handleDic.Add(key, asyncOperation);
            callback.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback = null) where T : Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int curCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in opHandle.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    callback.Invoke(result.PrimaryKey, curCount, totalCount);
                });
            }
        };
    }

    public void Clear()
    {
        resourceDic.Clear();

        foreach (var op in handleDic)
            Addressables.Release(op);

        handleDic.Clear();
        
    }
    #endregion
}
