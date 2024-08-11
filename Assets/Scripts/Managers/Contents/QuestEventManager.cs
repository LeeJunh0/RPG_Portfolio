using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventManager
{
    public Action<Define.EQuestEvent> QuestEvent = null;

    public void QuestUpdate(Define.EQuestEvent evt)
    {
        if (QuestEvent == null)
            return;

        switch (evt)
        {
            case Define.EQuestEvent.Kill:
                QuestEvent.Invoke(evt);
                break;
            case Define.EQuestEvent.Get:
                break;
            case Define.EQuestEvent.Level:
                break;
        }
    }
}
