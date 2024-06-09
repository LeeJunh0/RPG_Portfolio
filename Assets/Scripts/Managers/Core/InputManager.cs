using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool pressed = false;
    float pressedTime = 0f;

    public void OnUpdate()
    {
        if(Input.anyKey &&KeyAction != null)
        {
            KeyAction.Invoke();
        }

        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                if (pressed == false) 
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    pressedTime += Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                pressed = true;
            }
            else
            {
                if(pressed == true)
                {
                    if(Time.time < pressedTime + 0.2f)  
                        MouseAction.Invoke(Define.MouseEvent.Click);

                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
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

