using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : UIBase
{
    enum Buttons 
    { 
        InitButton,
        InstButton,
        MoveButton
    }

    public int initIndex = 0;
    public int InstIndex = 0;
    public int MoveIndex = 0;

    public override void Init()
    {
        BindObject(typeof(Buttons));

    }
}
