using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMaker
{
    public Image icon;

    public virtual void Init() { }
    public void SetInfo(Image _icon) { icon.sprite = _icon.sprite; }
    public virtual void SkillInit() { }
    public virtual void SkillInstantiate() { }
    public virtual void SkillProperties() { }
}
