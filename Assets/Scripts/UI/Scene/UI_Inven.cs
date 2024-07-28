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
        temp1,
        temp2,
        temp3
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.Sorting).BindEvent((evt) => 
        { 
            Debug.Log("정렬버튼 on");
            Managers.Inventory.ItemSort();
        });
        GetObject((int)GameObjects.Create).BindEvent((evt) => 
        { 
            Managers.Inventory.AddItem(Managers.Data.ItemDict[Random.Range(102, 106)]);
        });
        GetObject((int)GameObjects.Delete).BindEvent((evt) =>
        {
            Managers.Inventory.DeleteItem();
        });
        GetObject((int)GameObjects.temp1).BindEvent((evt) =>
        {
            Debug.Log("temp1 on");
        });
        GetObject((int)GameObjects.temp2).BindEvent((evt) =>
        {
            Debug.Log("temp2 on");
        });
        GetObject((int)GameObjects.temp3).BindEvent((evt) =>
        {
            Debug.Log("temp3 on");
        });     
    }

    private void Start()
    {
        Managers.Inventory.Interlocking();
    }
}
