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
            //���� SelectIndex���� Quest�� Invoke�� �־��ָ� �ɵ�. -> �̰� �Ұ����� ���ʿ� Giver�� ������ ������������.
            //�׷��� QuestGiver���� ĸ��ȭ �Ѱ��� �ƴ� QuestManager�� ���� �����ؼ� Invoke��Ű�°ɷ� ��.
            Managers.Quest.OnStartQuest?.Invoke(curQuest);
        });
    }

    public void GiverPopupInit()
    {
        gameObject.SetActive(true);

        GetObject((int)GiverPopupObject.GiverQuestName).GetComponent<Text>().text = curQuest.name;
        GetObject((int)GiverPopupObject.GiverQuestDescript).GetComponent<Text>().text = curQuest.description;

        Text rewards = GetObject((int)GiverPopupObject.GiverQuestRewards).GetComponent<Text>();
        rewards.text = string.Format($"���� : {curQuest.rewards.experience}");
        foreach (string item in curQuest.rewards.items)
            rewards.text += string.Format($",{item} ");
    }

    public void SetInfo(QuestInfo quest) { curQuest = quest; }
}
