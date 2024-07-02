using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest : UIPopup
{
    Queue<Data.Quest> questQue = new Queue<Data.Quest>();

    private void OnEnable()
    {
        GameObject questList = Util.FindChild<GridLayoutGroup>(gameObject, "QuestList").gameObject;

        if (questList.transform.childCount <= 0)
            return;

        //foreach (Quest quest in questQue)
        //{

        //}
    }
}
