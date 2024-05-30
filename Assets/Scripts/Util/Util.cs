using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T Component = go.GetComponent<T>();
        if (Component == null)
        {
            Component = go.AddComponent<T>();
        }
        return Component;
    }

    public static GameObject FindChild(GameObject parent, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(parent, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject parent, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (parent == null)
            return null;

        if(recursive == false)
        {
            for(int i = 0; i < parent.transform.childCount; i++)
            {
                Transform transform = parent.transform.GetChild(i);

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in parent.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }
}
