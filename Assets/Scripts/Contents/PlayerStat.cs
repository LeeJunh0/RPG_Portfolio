using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int exp; 
    [SerializeField]
    protected int totalExp;
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
                Managers.UI.UIStatUpdate?.Invoke(Level);
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
        gold = 1000000;

        SetStat(level);
        StartCoroutine(Regenerate());
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        hp = stat.hp;
        maxHp = stat.hp;
        mp = stat.mp;
        maxMp = stat.mp;
        manaRecovery = stat.manaRecovery;
        attack = stat.attack;
        exp = 0;

        if(Managers.Data.StatDict.TryGetValue(level + 1, out stat) == true)
            totalExp = stat.totalExp;
    }

    public void OnHealing(int heal)
    {
        Hp = Mathf.Clamp(Hp + heal, 0, maxHp);
    }

    IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ManaRegen();
        }
    }

    private void ManaRegen()
    {
        if (mp < maxMp)
        {
            mp += manaRecovery;
            mp = Mathf.Clamp(mp, 0, maxMp);
        }       
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead!");
        StopCoroutine(Regenerate());
    }

    public int GetTotalExp() { return totalExp; }
}
