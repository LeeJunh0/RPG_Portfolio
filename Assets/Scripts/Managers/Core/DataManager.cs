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
    public Dictionary<int, QuestInfo> QuestDict { get; private set; } = new Dictionary<int, QuestInfo>();
    public Dictionary<int, Iteminfo> ItemDict { get; private set; } = new Dictionary<int, Iteminfo>();
    public Dictionary<string, DropInfo> DropDict { get; private set; } = new Dictionary<string, DropInfo>();
    public Dictionary<string, SkillInitInfo> SkillInitDict { get; private set; } = new Dictionary<string, SkillInitInfo>();
    public Dictionary<string, SkillInstantiateInfo> SkillInstDict { get; private set; } = new Dictionary<string, SkillInstantiateInfo>();
    public Dictionary<string, SkillMoveInfo> SkillMoveDict { get; private set; } = new Dictionary<string, SkillMoveInfo>();
    public void Init()
    {
        StatDict = LoadJson<StatData, int, Data.Stat>("StatData").MakeDict();        
        QuestDict = LoadJson<QuestData, int, QuestInfo>("QuestData").MakeDict();
        ItemDict = LoadJson<ItemData, int, Iteminfo>("ItemsData").MakeDict();
        DropDict = LoadJson<DropData, string, DropInfo>("DropData").MakeDict();
        SkillInitDict = LoadJson<Skill_InitData, string, SkillInitInfo>("SkillInitData").MakeDict();
        SkillInstDict = LoadJson<Skill_InstData, string, SkillInstantiateInfo>("SkillInstData").MakeDict();
        SkillMoveDict = LoadJson<Skill_MoveData, string, SkillMoveInfo>("SkillMoveData").MakeDict();
    }

    T LoadJson<T, Key, Value>(string path) where T : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
