using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BindKey
{
    public static KeyCode Inventory { get; private set; }
    public static KeyCode Quest { get; private set; }
    public static KeyCode Skill { get; private set; }
    public static KeyCode Pause { get; private set; }

    public static void Init()
    {
        Inventory = KeyCode.Tab;
        Quest = KeyCode.Q;
        Skill = KeyCode.K;
        Pause = KeyCode.T;
    }
}
