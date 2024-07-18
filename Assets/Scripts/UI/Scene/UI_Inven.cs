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
        GridPanel,
        CreateButton,
        DeleteButton,
        SortingButton
    }

    GameObject inven;
    Data.Iteminfo[] invenList;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        inven = Util.FindChild(gridPanel, "Content", true);
        GetObject((int)GameObjects.CreateButton).BindEvent((evt) => { CreateItem(); });
        GetObject((int)GameObjects.DeleteButton).BindEvent((evt) => { DeleteItem(); });
        GetObject((int)GameObjects.SortingButton).BindEvent((evt) => { Debug.Log("정렬버튼 on"); });

        InventoryInit();
    }

    void InventoryInit()
    {
        invenList = new Data.Iteminfo[inven.transform.childCount];

        for (int i = 0; i < invenList.Length; i++)
        {
            if (invenList[i] == null)
            {
                invenList[i] = Managers.Data.ItemDict[101];
                inven.transform.GetChild(i).GetOrAddComponent<UI_Inven_Item>().SetInfo(Managers.Data.ItemDict[101]);
                continue;
            }

            inven.transform.GetChild(i).GetOrAddComponent<UI_Inven_Item>().SetInfo(invenList[i]);
        }
    }

    void CreateItem()
    {
        int rand = 105;
        //int rand = Random.Range(102, 100 + Managers.Data.ItemDict.Count);

        for (int i = 0; i < invenList.Length; i++)
        {
            if (invenList[i].id > 101)
            {
                if (invenList[i].uiInfo.isStack == true)
                {
                    for(int j = 0; j < invenList.Length; j++)
                    {
                        if (invenList[i].id == invenList[j].id)
                        {
                            inven.transform.GetChild(j).GetOrAddComponent<UI_Inven_Item>().MyStack++;
                            return;
                        }
                    }
                }                              
            }

            invenList[i] = Managers.Data.ItemDict[rand];
            inven.transform.GetChild(i).GetOrAddComponent<UI_Inven_Item>().SetInfo(invenList[i]);
            Debug.Log($"{invenList[i].uiInfo.name}");
            return;
        }
        Debug.Log("빈칸이 없어 생성 하지 못했습니다.");
    }

    void DeleteItem() 
    {
        for (int i = invenList.Length - 1; i >= 0; i--)
        {
            if (invenList[i].id > 101)
            {
                if (invenList[i].uiInfo.isStack == false)
                {
                    Debug.Log($"지워진 아이템 이름 : {invenList[i].uiInfo.name}");
                    invenList[i] = Managers.Data.ItemDict[101];
                    inven.transform.GetChild(i).GetOrAddComponent<UI_Inven_Item>().SetInfo(invenList[i]);
                    return;
                }  
            }
        }
    }

    void SortingItem()
    {

    }
}
