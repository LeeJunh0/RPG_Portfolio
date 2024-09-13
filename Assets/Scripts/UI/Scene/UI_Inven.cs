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
        UI_Inven_Popup
    }

    GameObject InvenUI;
    UI_Inven_Item[] iconInfos;
    GameObject popup;
    int curIndex = int.MaxValue;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        InvenUI = Util.FindChild(this.gameObject, "Content", true);
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
        iconInfos = new UI_Inven_Item[Managers.Option.InventoryCount];

        for(int i = 0; i < iconInfos.Length; i++)
        {
            iconInfos[i] = InvenUI.transform.GetChild(i).GetOrAddComponent<UI_Inven_Item>();
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
