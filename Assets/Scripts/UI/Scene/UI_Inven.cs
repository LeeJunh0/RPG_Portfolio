using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        UI_Inven_Sorting,
        UI_Inven_Popup,
        Content
    }
    int curIndex        = int.MaxValue;
    UI_Inven_Slot[]     iconInfos;
    GameObject          popup;


    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        popup = GetObject((int)GameObjects.UI_Inven_Popup);

        GetObject((int)GameObjects.UI_Inven_Sorting).BindEvent((evt) =>
        {
            Debug.Log("정렬버튼 on");
            Managers.Inventory.SortAll();
        });

        InfosInit();
        popup.SetActive(false);
    }

    private void Start()
    {
        Managers.Inventory.SetInvenReference(iconInfos);
        Managers.Inventory.InterLocking();
    }

    public void InfosInit()
    {
        iconInfos = new UI_Inven_Slot[Managers.Option.InventoryCount];

        foreach (Transform child in GetObject((int)GameObjects.Content).transform)
            Managers.Resource.Destroy(child.gameObject);

        for(int i = 0; i < iconInfos.Length; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI_Inven_Slot");
            item.transform.SetParent(GetObject((int)GameObjects.Content).transform);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
            iconInfos[i] = item.GetOrAddComponent<UI_Inven_Slot>();
            iconInfos[i].SetIndex(i);
            
        }
    }

    public void OnInvenPopup(int index)
    {
        Iteminfo iteminfo = Managers.Inventory.InvenInfos[index];
        popup.GetComponent<UI_Inven_Popup>().InvenPopupInit(iteminfo);

        if (curIndex != index)
        {
            curIndex = index;
            return;
        }

        curIndex = int.MaxValue;
        popup.SetActive(false);
    }
}
