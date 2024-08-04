using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven : UIScene
{
    enum GameObjects
    {
        Sorting,
        Create,
        Delete,
    }

    GameObject InvenUI;
    UI_Inven_Item[] iconInfos;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));      
        InvenUI = Util.FindChild(this.gameObject, "Content", true);
        GetObject((int)GameObjects.Sorting).BindEvent((evt) =>
        {
            Debug.Log("정렬버튼 on");
            Managers.Inventory.SortAll();
        });
        GetObject((int)GameObjects.Create).BindEvent((evt) =>
        {
            Debug.Log("생성버튼 on");
            Managers.Inventory.AddItem(Managers.Data.ItemDict[Random.Range(102, 106)]);
        });
        GetObject((int)GameObjects.Delete).BindEvent((evt) =>
        {
            Debug.Log("제거버튼 on");
            Managers.Inventory.RemoveItem(Managers.Inventory.SelectIndex);
        });

        InfosInit();
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
}
