using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Stat : MonoBehaviour
{    
    [SerializeField]
    protected int level;
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int maxHp;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected int defense;
    [SerializeField]
    protected float movespeed;

    public int Level { get { return level; } set { level = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public float Movespeed { get { return movespeed; } set { movespeed = value; } }

    private void Start()
    {
        level = 1;
        hp = 100;
        maxHp = 100;
        attack = 6;
        defense = 5;
        movespeed = 5.0f;
    }

    public virtual void OnDamaged(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if(Hp <= 0)
        {
            Hp = 0;
            
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if(playerStat != null)
        {
            DropInfo drop;
            if (Managers.Data.DropDict.TryGetValue(gameObject.name, out drop) == true)
            {
                playerStat.Exp += drop.drops.experience;
                DropEvent(drop);
            }               
        }

        Managers.Quest.OnKillQuestAction?.Invoke(this.name);
        Managers.Game.Despawn(gameObject);
    }

    protected void DropEvent(DropInfo drop)
    {
        for (int i = 0; i < Managers.Data.ItemDict.Count - 1; i++)
        {
            for (int j = 0; j < drop.drops.items.Count; j++)
            {
                if (Managers.Data.ItemDict[102 + i].uiInfo.name == drop.drops.items[j])
                {
                    Managers.Inventory.AddItem(new Iteminfo(Managers.Data.ItemDict[102 + i]));
                }
            }
        }
    }
}
