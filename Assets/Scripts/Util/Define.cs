using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum EWorldObject
    {
        Unknown = 0,
        Player,
        Monster
    }
    public enum EState
    {
        Die,
        Idle,
        Move,
        Skill,    
        Dodge,
        HardAttack,
        GroundAttack
    }

    public enum ELayer
    {
        Ground = 6,
        Monster = 7,
        Block = 8,
        NPC = 9
    }

    public enum EIndicator
    {
        RangeIndicator = 0,
        CircleIndicator,
        ArrowIndicator,
        EnemyArcIndicator,
        EnemyBoxIndicator,
        EnemyCircleIndicator
    }

    public enum ESkill
    {
        Projectile = 0,
        AreaOfEffect,
        Assault
    }

    public enum ESkill_Elemental
    {
        None,
        Fire,
        Ice,
        Wind
    }

    public enum EMotifyType
    {
        Initialize,
        Embodiment,
        Movement
    }

    public enum ENpc
    {
        Giver,
        Trader,
        Normal
    }


    public enum EScene
    {
        Title,
        Game
    }

    public enum ESound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum EMouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum EUiEvent
    {
        PointerEnter,
        PointerExit,
        Click,
        BeginDrag,
        Drag,
        EndDrag,
        Drop,
        Up
    }

    public enum EQuestEvent
    {
        Kill,
        Get,
        Level
    }

    public enum ECameraMode
    {
        QuaterView,
    }

    public enum ECameraType
    {
        MainCamera,
        MiniMapCamera
    }
}
