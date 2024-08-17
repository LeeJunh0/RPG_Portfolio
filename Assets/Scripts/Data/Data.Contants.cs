using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int hp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
    #endregion

    #region Quest
    [Serializable]
    public class QuestInfo
    {
        public int id;
        public string name;
        public string description;
        public bool isCompleted;
        public Task task;
        public int successCount;
        public Rewards rewards;

        public QuestInfo(QuestInfo info)
        {
            this.id = info.id;
            this.name = info.name;
            this.description = info.description;
            this.isCompleted = info.isCompleted;
            this.task = info.task;
            this.rewards = info.rewards;
        }

    }

    [Serializable]
    public class Task
    {
        public int type;
        public string target;
        public int currentSuccess;
    }

    [Serializable]
    public class Rewards
    {
        public int experience;
        public List<string> items;
    }

    [Serializable]
    public class QuestData : ILoader<int, QuestInfo>
    {
        public List<QuestInfo> quests = new List<QuestInfo>();

        public Dictionary <int, QuestInfo> MakeDict()
        {
            Dictionary<int, QuestInfo> dict = new Dictionary<int, QuestInfo>();

            foreach(QuestInfo quest in quests)
                dict.Add(quest.id, quest);

            return dict;
        }
    }
    #endregion

    #region Item
    [Serializable]
    public class Iteminfo : ItemStack
    {
        public int id;
        public int hp;
        public int att;
        public int gold;
        public ItemUIinfo uiInfo;

        public Iteminfo(Iteminfo info)
        {
            this.id = info.id;
            this.hp = info.hp;
            this.att = info.att;
            this.gold = info.gold;
            this.uiInfo = info.uiInfo;
        }
    }

    [Serializable]
    public class ItemUIinfo
    {
        public string icon;
        public string name;
        public string description;
        public bool isStack;
    }
    
    [Serializable]
    public class ItemData : ILoader<int, Iteminfo>
    { 
        public List<Iteminfo> items = new List<Iteminfo>();

        public Dictionary<int, Iteminfo> MakeDict()
        {
            Dictionary<int,Iteminfo> dict = new Dictionary<int, Iteminfo>();

            foreach(Iteminfo item in items)
                dict.Add(item.id, item);
            
            return dict;
        }
    }
    #endregion
}

