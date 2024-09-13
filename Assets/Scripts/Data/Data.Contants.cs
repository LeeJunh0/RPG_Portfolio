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
        public int successCount;
        public Task task;
        public Rewards rewards;

        public QuestInfo(QuestInfo info)
        {
            this.id = info.id;
            this.name = info.name;
            this.description = info.description;
            this.successCount = info.successCount;
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
    public enum ItemType
    {
        None,
        Consumable,
        Attachment
    }

    [Serializable]
    public class Iteminfo : ItemStack
    {        
        public int id;
        public int hp;
        public int att;
        public int gold;
        public ItemType type;
        public ItemUIinfo uiInfo;

        public Iteminfo(Iteminfo info)
        {
            this.id = info.id;
            this.hp = info.hp;
            this.att = info.att;
            this.gold = info.gold;
            this.type = info.type;
            this.uiInfo = info.uiInfo;
        }

        public string GetItemName() { return uiInfo.name; }

        public void OnUsing()
        {
            switch (type)
            {
                case ItemType.None:
                    break;
                case ItemType.Consumable:
                    UseConsumableItem();
                    break;
                case ItemType.Attachment:
                    break;
            }
        }

        private void UseConsumableItem()
        {           
            Managers.Game.GetPlayer().GetComponent<PlayerStat>().Hp += hp;
            MyStack--; 
        }

        private void UseEquipItem()
        {
            
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

    #region Drop
    [Serializable]
    public class DropInfo
    {
        public string name;
        public Rewards drops;
        public int sliver;
    }

    [Serializable]
    public class DropData : ILoader<string, DropInfo>
    {
        public List<DropInfo> drops = new List<DropInfo>();
        public Dictionary<string, DropInfo> MakeDict()
        {
            Dictionary<string, DropInfo> dict = new Dictionary<string, DropInfo>();

            foreach (DropInfo drop in drops)
                dict.Add(drop.name, drop);

            return dict;
        }
    }
    #endregion
}

