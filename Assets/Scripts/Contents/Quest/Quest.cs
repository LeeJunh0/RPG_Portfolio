using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    string questName;
    string description;
    bool isComplete;
    Task task;
    int successCount;
    Rewards rewards;

    public string QuestName => questName;
    public string Description => description;
    public bool IsComplete { get { return isComplete; } private set { isComplete = value; } }
    public Task Task { get { return task; } private set { task = value; } }
    public Rewards Rewards => rewards;
    public int SuccessCount => successCount;

    public Quest(QuestInfo questInfo)
    {
        this.questName = questInfo.name;
        this.description = questInfo.description;
        isComplete = false;
        Task = new Task(questInfo.task.type, questInfo.task.target, questInfo.task.currentSuccess);
        successCount = questInfo.successCount;
        rewards = questInfo.rewards;

        task.OnCurrentUpdate -= CheckingTask;
        task.OnCurrentUpdate += CheckingTask;
    }

    public void CheckingTask() { IsComplete = task.CurrentSuccess >= successCount; }
    public void Complete() { task.Complete(this); }
}
