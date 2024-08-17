using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int exp;
    [SerializeField]
    protected int gold;

    public int Exp
    {
        get { return exp; }
        set
        { 
            exp = value;

            int level = Level;
            while(true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (exp < stat.totalExp)
                    break;
                level++;
            }

            if(level != Level)
            {
                Debug.Log("Level UP!");
                Level = level;

                SetStat(Level);
                Managers.Quest.OnLevelQuestAction?.Invoke(Level);
            }
        }
    }

    public int Gold { get { return gold; } set { gold = value; } }

    private void Start()
    {
        level = 1;
        defense = 5;
        movespeed = 5.0f;
        exp = 0;
        gold = 0;

        SetStat(level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        hp = stat.hp;
        maxHp = stat.hp;
        attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead!");
    }
}
