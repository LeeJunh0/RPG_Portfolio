using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BindKey
{
    public static KeyCode Inventory { get; private set; }
    public static KeyCode Equipment { get; private set; }
    public static KeyCode Quest { get; private set; }
    public static KeyCode Skill { get; private set; }
    public static KeyCode Pause { get; private set; }
    public static KeyCode SkillSlot_1 { get; set; }
    public static KeyCode SkillSlot_2 { get; set; }
    public static KeyCode SkillSlot_3 { get; set; }
    public static KeyCode Interact { get; set; }
    public static void Init()
    {
        Inventory = KeyCode.Tab;
        Equipment = KeyCode.E;
        Quest = KeyCode.L;
        Skill = KeyCode.K;
        Pause = KeyCode.Escape;
        SkillSlot_1 = KeyCode.A;
        SkillSlot_2 = KeyCode.S;
        SkillSlot_3 = KeyCode.D;
        Interact = KeyCode.Space;
    }
}
