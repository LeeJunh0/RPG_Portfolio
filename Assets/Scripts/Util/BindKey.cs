using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BindKey
{
    public static KeyCode Inventory;
    public static KeyCode Quest;
    public static KeyCode Skill;
    public static KeyCode Pause;

    public static void Init()
    {
        Inventory = KeyCode.Tab;
        Quest = KeyCode.Q;
        Skill = KeyCode.K;
        Pause = KeyCode.T;
    }
}
