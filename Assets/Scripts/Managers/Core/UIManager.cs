using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    public Action<int> UIStatUpdate = null;
    public Stack<UIPopup> popupStack = new Stack<UIPopup>();
    private int sortOrder = 10;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UIRoot");

            if (root == null)
                root = new GameObject { name = "UIRoot" };
            return root;
        }
    }

    public Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = sortOrder;
            sortOrder++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public Transform SearchTopCanvas(GameObject ui)
    {
        Canvas parentcanvas = ui.GetComponentInParent<Canvas>();

        while(parentcanvas == null && parentcanvas.transform.parent != null)
        {
            parentcanvas = parentcanvas.GetComponentInParent<Canvas>();
        }

        return parentcanvas.transform;
    }

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if(string.IsNullOrEmpty(name))       
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate(name);
        T poPup = Util.GetOrAddComponent<T>(go);
        popupStack.Push(poPup);
        go.transform.SetParent(Root.transform);
        return poPup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate(name);
        T sceneUI = Util.GetOrAddComponent<T>(go);

        go.transform.SetParent(Root.transform);
        return sceneUI;
    }

    public T MakeSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name)) 
            name = typeof(T).Name;

        GameObject go = Util.FindChild(Root);
        if (go == null)
            go = Managers.Resource.Instantiate(typeof(T).Name);
        
        T EScene = Util.GetOrAddComponent<T>(go);

        go.transform.SetParent(Root.transform);
        return EScene;
    }

    public T MakeSubItem<T>(Transform parent = null ,string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate(name);

        if (parent != null)
        {
            go.transform.SetParent(parent);
        }

        return go.GetOrAddComponent<T>();
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))        
            name = typeof(T).Name;       

        GameObject go = Managers.Resource.Instantiate(name);
        if (parent != null)        
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
       
        return go.GetOrAddComponent<T>();
    }

    public void OnGameUIPopup<T>(GameObject parent = null) where T : UIPopup
    {
        T uiType = Util.FindChild<T>(Root);
        Debug.Log($"On{typeof(T)}");

        if (uiType != null)
            ClosePopupUI(uiType);
        else
            ShowPopupUI<T>();
    }


    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        sortOrder--;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (popupStack.Count == 0)
            return;

        if (popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failded!");
            return;
        }

        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            popupStack.Pop();
    }

    public void Clear()
    {
        CloseAllPopupUI();
    }
}
