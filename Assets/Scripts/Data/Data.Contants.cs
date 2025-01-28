using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Define;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int hp;
        public int mp;
        public int manaRecovery;
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
        public int      id;
        public string   name;
        public string   description;
        public int      successCount;
        public Task     task;
        public Rewards  rewards;

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
        public int      type;
        public string   target;
        public int      currentSuccess;
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
    public class Iteminfo
    {        
        public int          id;
        public int          hp;
        public int          att;
        public int          gold;
        public ItemType     type;
        public bool         isStack;
        public int          curStack;
        public ItemUIinfo   uiInfo;

        public Iteminfo(Iteminfo info)
        {
            this.id = info.id;
            this.hp = info.hp;
            this.att = info.att;
            this.gold = info.gold;
            this.type = info.type;
            this.isStack = info.isStack;
            this.curStack = info.curStack;
            this.uiInfo = info.uiInfo;
        }

        public string GetItemName() { return uiInfo.name; }
    }

    [Serializable]
    public class ItemUIinfo
    {
        public string icon;
        public string name;
        public string description;
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
        public string   name;
        public Rewards  drops;
        public int      sliver;
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

    #region Skill
    [Serializable]
    public class SkillInfo
    {
        public string           name;
        public EWorldObject     useObject;
        public EIndicator       indicator;
        public ESkill           type;
        public int              level;
        public float            damage;
        public float            mana;
        public float            speed;
        public float            length;
        public float            radius;
        public float            coolTime;
        public bool             isActive = true;     
        public string           icon;
        public string           function;
        public string           description;

        public SkillInfo(SkillInfo refInfo)
        {
            name = refInfo.name;
            useObject = refInfo.useObject; 
            indicator = refInfo.indicator;
            type = refInfo.type;
            level = refInfo.level;
            damage = refInfo.damage;
            mana = refInfo.mana;
            speed = refInfo.speed;
            length = refInfo.length;
            radius = refInfo.radius;
            coolTime = refInfo.coolTime;
            isActive = refInfo.isActive;    
            icon = refInfo.icon;
            function = refInfo.function;
            description = refInfo.description;
        }
    }

    [Serializable]
    public class SkillData : ILoader<string, SkillInfo>
    {
        public List<SkillInfo> skills = new List<SkillInfo>();
        public Dictionary<string, SkillInfo> MakeDict()
        {
            Dictionary<string, SkillInfo> dict = new Dictionary<string, SkillInfo>();

            foreach (SkillInfo skill in skills)
                dict.Add(skill.name, skill);

            return dict;
        }
    }
    #endregion
    #region Motify

    [Serializable]
    public class MotifyInfo
    {
        public int          id;
        public string       skillName;
        public ESkill       owner;
        public EMotifyType  type;
        public int          mana; 
        public string       name;
        public string       icon;
        public string       function;
        public string       description;

        public MotifyInfo(MotifyInfo refinfo)
        {
            id = refinfo.id;
            skillName = refinfo.skillName;
            type = refinfo.type;
            mana = refinfo.mana;
            name = refinfo.name;
            icon = refinfo.icon;
            function = refinfo.function;
            description = refinfo.description;
        }
    }

    [Serializable]
    public class MotifyData : ILoader<int, MotifyInfo>
    {
        public List<MotifyInfo> motifys = new List<MotifyInfo>();

        public Dictionary<int, MotifyInfo> MakeDict()
        {
            Dictionary<int, MotifyInfo> dict = new Dictionary<int, MotifyInfo>();
            foreach (MotifyInfo motify in motifys)
                dict.Add(motify.id, motify);

            return dict;
        }
    }
    #endregion
}

