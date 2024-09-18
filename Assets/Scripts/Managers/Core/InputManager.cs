using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public event Action KeyAction = null;
    public event Action<Define.EMouseEvent> MouseAction = null;

    bool pressed = false;
    float pressedTime = 0f;

    public void OnUpdate()
    {
        if (KeyAction != null && Input.anyKey)       
            KeyAction.Invoke();

        if (Input.GetKeyDown(KeyCode.Space))
            Managers.Inventory.AddItem(new Data.Iteminfo(Managers.Data.ItemDict[UnityEngine.Random.RandomRange(102, 107)]));

        if (MouseAction != null)
        {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (pressed == false) 
                {
                    MouseAction.Invoke(Define.EMouseEvent.PointerDown);
                    pressedTime += Time.time;
                }
                MouseAction.Invoke(Define.EMouseEvent.Press);
                pressed = true;
            }
            else
            {
                if(pressed == true)
                {
                    if(Time.time < pressedTime + 0.2f)  
                        MouseAction.Invoke(Define.EMouseEvent.Click);

                    MouseAction.Invoke(Define.EMouseEvent.PointerUp);
                }
                pressed = false;
                pressedTime = 0f;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}

