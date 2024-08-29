using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    int type;
    string target;
    int currentSuccess;
    public event Action OnCurrentUpdate = null;

    public Task(int _type, string _target, int _current)
    {
        type = _type;
        target = _target;
        currentSuccess = _current;
    }

    public int Type => type;
    public string Target => target;
    public int CurrentSuccess
    {
        get { return currentSuccess; }
        private set
        {
            currentSuccess = value;
            OnCurrentUpdate?.Invoke();
            Managers.Quest.OnCurrentUpdate?.Invoke(CurrentSuccess);
        }
    }

    public Define.EQuestEvent QuestEvent => (Define.EQuestEvent)type;

    public void Counting(string target)
    {
        if (this.target.Contains(target) == false)
            return;

        CurrentSuccess++;
    }

    public void CountSet(string target, int count)
    {
        if (this.target.Contains(target) == false || count < 0)
            return;

        CurrentSuccess = count;
    }

    public void Complete(Quest quest) { Managers.Quest.OnCompletedQuest?.Invoke(quest); }
}
