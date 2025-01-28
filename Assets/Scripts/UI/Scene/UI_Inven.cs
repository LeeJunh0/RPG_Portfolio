using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        UI_Inven_Sorting,
        UI_Sliver_Text,
        Content
    }

    UI_Inven_Slot[] iconInfos;
    Text sliverText;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        sliverText = GetObject((int)GameObjects.UI_Sliver_Text).GetComponent<Text>();

        GetObject((int)GameObjects.UI_Inven_Sorting).BindEvent((evt) =>
        {
            Debug.Log("정렬버튼 on");
            Managers.Inventory.SortAll();
        });

        InfosInit();
    }

    private void Start()
    {
        Managers.Inventory.SetInvenReference(iconInfos);
        Managers.Inventory.InterLocking();
    }

    private void Update()
    {
        SetSliverText();     
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

    public void SetSliverText() 
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        int curSliver = playerStat.Gold;
 
        sliverText.text = string.Format("{0:#,0}", curSliver);
    }
}
