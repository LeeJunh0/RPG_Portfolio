using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject player;
    HashSet<GameObject> monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return player; }

    public GameObject Spawn(Define.EWorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.EWorldObject.Monster:
                monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.EWorldObject.Player:
                player = go;
                break;
        }
        return go;
    }

    public Define.EWorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc.WorldObjectType == null)
            return Define.EWorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.EWorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.EWorldObject.Unknown:
                break;
            case Define.EWorldObject.Player:
                {
                    if (player == go)
                        player = null;                    
                }
                break;
            case Define.EWorldObject.Monster:
                {
                    if (monsters.Contains(go) == true)
                    {
                        monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }        
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
