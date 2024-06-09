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

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                player = go;
                break;
        }
        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc.WorldObjectType == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Unknown:
                break;
            case Define.WorldObject.Player:
                {
                    if (player == go)
                        player = null;                    
                }
                break;
            case Define.WorldObject.Monster:
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
