using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{
    string          name;   
    float           damage;
    float           mana;
    float           coolTime;
    bool            isActive = true;
    Define.ESkill   type = Define.ESkill.Projectile;

    public string           Name        { get { return name; } set { name = value; } }
    public Define.ESkill    Type        { get { return type; } set { type = value; } }
    public float            Damage      { get { return damage; } set { damage = value; } }
    public float            Mana        { get { return mana; } set { mana = value; } }
    public float            CoolTime    { get { return coolTime; } set { coolTime = value; } }
    public bool             IsActive    { get { return isActive;} set { isActive = value; } }
}
