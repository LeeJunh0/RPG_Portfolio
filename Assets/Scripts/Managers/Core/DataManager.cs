using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key,Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public Dictionary<int, Data.Quest> QuestDict { get; private set; } = new Dictionary<int, Data.Quest>();
    public Dictionary<int, Data.Iteminfo> ItemDict { get; private set; } = new Dictionary<int, Iteminfo>();
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();        
        QuestDict = LoadJson<Data.QuestData, int, Data.Quest>("QuestData").MakeDict();
        ItemDict = LoadJson<Data.ItemData, int, Data.Iteminfo>("ItemsData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
