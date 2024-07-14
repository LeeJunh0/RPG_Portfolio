using System;
using System.Collections;
using System.Collections.Generic;
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
    public class Quest
    {
        public int id;
        public string name;
        public string description;
        public bool isCompleted;
        public Rewards rewards;
    }

    [Serializable]
    public class Rewards
    {
        public int experience;
        public List<string> items;
    }

    [Serializable]
    public class QuestData : ILoader<int, Quest>
    {
        public List<Quest> quests = new List<Quest>();

        public Dictionary <int, Quest> MakeDict()
        {
            Dictionary<int, Quest> dict = new Dictionary<int, Quest>();

            foreach(Quest quest in quests)
                dict.Add(quest.id, quest);

            return dict;
        }
    }
    #endregion

    #region Item
    [Serializable]
    public class Iteminfo 
    {
        public int id;
        public int hp;
        public int att;
        public int gold;
        public ItemUIinfo uiInfo;
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

