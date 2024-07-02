using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IStatLoader<Key,Value>
{
    Dictionary<Key, Value> MakeDict();
}

public interface IQuestLoader<Value>
{
    Dictionary<object, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<object, Data.Stat> StatDict { get; private set; } = new Dictionary<object, Data.Stat>();
    public Dictionary<object, Data.Quest> QuestDict { get; private set; } = new Dictionary<object, Data.Quest>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, object, Data.Stat>("StatData").MakeDict();
        QuestDict = LoadJson<Data.QuestData, Data.Quest>("QuestData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : IStatLoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    Loader LoadJson<Loader, Value>(string path) where Loader : IQuestLoader<Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
