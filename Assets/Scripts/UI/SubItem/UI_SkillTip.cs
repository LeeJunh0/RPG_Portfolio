using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTip : MonoBehaviour
{
    static UI_SkillTip      instance;
    public CanvasGroup      group;
    public RectTransform    parentRect;
    public Image            icon;
    public Text             skillName;
    public Text             skillFuction;
    public Text             skillStat;
    public Text             skillDescription;
    public Text             initMotify;
    public Text             embodiMotify;
    public Text             moveMotify;

    public static UI_SkillTip Instance => instance;

    void Start()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void SetToolTip(SkillInfo info)
    {
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.icon);
        icon.sprite = Managers.UI.TextureToSprite(texture);

        skillName.text = string.Format($"{info.name}");
        skillStat.text = string.Format($"¸¶³ª : {info.mana}");
        skillFuction.text = string.Format($"{info.function}");
        skillDescription.text = string.Format($"{info.description}");
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
