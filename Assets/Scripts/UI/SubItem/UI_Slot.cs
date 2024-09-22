using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Slot : UIBase
{
    protected enum Images { SlotIcon, }
    public Image icon;

    public override void Init()
    {
        SetInfo();
    }

    public virtual void SetInfo()
    {
        BindImage(typeof(Images));
    }

    public void SetIcon(Sprite _icon) { icon.sprite = _icon; }
}
