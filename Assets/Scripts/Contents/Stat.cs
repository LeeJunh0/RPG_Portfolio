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
    protected float mp;
    [SerializeField] 
    protected float maxMp;
    [SerializeField]
    protected int manaRecovery;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected int defense;
    [SerializeField]
    protected float movespeed;

    public int Level { get { return level; } set { level = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float MaxMp { get { return maxMp; } set { maxMp = value; } }
    public int ManaRecovery { get { return manaRecovery; } set { manaRecovery = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public float Movespeed { get { return movespeed; } set { movespeed = value; } }
    public int Hp 
    {
        get { return hp; }
        set 
        {
            hp = value;

            PlayerStat playerStat = this as PlayerStat;            
            if (playerStat != null)
                Managers.Equip.OnStatusSet?.Invoke();
        } 
    }
    public float Mp 
    { 
        get { return mp; }
        set
        {
            mp = value;

            PlayerStat playerStat = this as PlayerStat;
            if (playerStat != null)
                return;

            Managers.Equip.OnStatusSet?.Invoke();
        } 
    }

    public virtual void OnDamaged(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        GameObject text = Managers.Resource.Instantiate("DamageText");
        text.GetComponent<UI_DamageText>().SetDamage(damage);
        text.transform.position = new Vector3(transform.position.x,text.transform.position.y,transform.position.z);

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
