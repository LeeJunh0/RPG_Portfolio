using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }
    public enum State
    {
        Die,
        Idle,
        Move,
        Skill
    }

    public enum Layer
    {
        Ground = 6,
        Monster = 7,
        Block = 8
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum EUiEvent
    {
        Click,
        Drag,
    }

    public enum CameraMode
    {
        QuaterView,
    }

}
