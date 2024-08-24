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

    public override void Init()
    {
        Bind<GameObject>(typeof(GiverPopupObject));

        
    }

    public void GiverPopupInit(QuestInfo quest)
    {
        gameObject.SetActive(true);

        GetObject((int)GiverPopupObject.GiverQuestName).GetComponent<Text>().text = quest.name;
        GetObject((int)GiverPopupObject.GiverQuestDescript).GetComponent<Text>().text = quest.description;

        Text rewards = GetObject((int)GiverPopupObject.GiverQuestRewards).GetComponent<Text>();
        rewards.text = string.Format($"���� : {quest.rewards.experience}");
        foreach (string item in quest.rewards.items)
            rewards.text += string.Format($",{item} ");

        GetObject((int)GiverPopupObject.AcceptButton).BindEvent((evt) =>
        {
            //���� SelectIndex���� Quest�� Invoke�� �־��ָ� �ɵ�. -> �̰� �Ұ����� ���ʿ� Giver�� ������ ������������.
            //�׷��� QuestGiver���� ĸ��ȭ �Ѱ��� �ƴ� QuestManager�� ���� �����ؼ� Invoke��Ű�°ɷ� ��.
            Managers.Quest.OnStartQuest?.Invoke(quest);
        });
    }
}
