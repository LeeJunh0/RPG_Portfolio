using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum EWorldObject
    {
        Unknown,
        Player,
        Monster
    }
    public enum EState
    {
        Die,
        Idle,
        Move,
        Skill
    }

    public enum ELayer
    {
        Ground = 6,
        Monster = 7,
        Block = 8
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
        Click,
        BeginDrag,
        Drag,
        EndDrag
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
}
