using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public class StatData : IStatLoader<object, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<object, Stat> MakeDict()
        {
            Dictionary<object, Stat> dict = new Dictionary<object, Stat>();

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
    public class QuestData : IQuestLoader<Quest>
    {
        public List<Quest> questList = new List<Quest>();

        public Dictionary <object, Quest> MakeDict()
        {
            Dictionary<object, Quest> dict = new Dictionary<object, Quest>();

            foreach(Quest quest in questList)
                dict.Add(quest.id, quest);

            return dict;
        }
    }
    #endregion
}

