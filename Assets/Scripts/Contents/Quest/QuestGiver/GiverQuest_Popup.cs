using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiverQuest_Popup : UIPopup
{
    enum GiverPopupObject
    {
        GiverQuestName,
        GiverQuestDescript,
        GiverQuestRewards,
        AcceptButton
    }

    QuestInfo curQuest;

    public override void Init()
    {
        Bind<GameObject>(typeof(GiverPopupObject));

        GetObject((int)GiverPopupObject.AcceptButton).BindEvent((evt) =>
        {
            //현재 SelectIndex값의 Quest를 Invoke에 넣어주면 될듯. -> 이건 불가능함 애초에 Giver에 접근이 가능하지않음.
            //그래서 QuestGiver에서 캡슐화 한것이 아닌 QuestManager에 직접 접근해서 Invoke시키는걸로 함.
            Managers.Quest.OnStartQuest?.Invoke(curQuest);
        });
    }

    public void GiverPopupInit()
    {
        gameObject.SetActive(true);

        GetObject((int)GiverPopupObject.GiverQuestName).GetComponent<Text>().text = curQuest.name;
        GetObject((int)GiverPopupObject.GiverQuestDescript).GetComponent<Text>().text = curQuest.description;

        Text rewards = GetObject((int)GiverPopupObject.GiverQuestRewards).GetComponent<Text>();
        rewards.text = string.Format($"보상 : {curQuest.rewards.experience}");
        foreach (string item in curQuest.rewards.items)
            rewards.text += string.Format($",{item} ");
    }

    public void SetInfo(QuestInfo quest) { curQuest = quest; }
}
