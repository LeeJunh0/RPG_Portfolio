using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTip : MonoBehaviour
{
    private static UI_SkillTip instance;

    public CanvasGroup group;
    public RectTransform parentRect;
    public Image icon;
    public Text skillName;
    public Text skillFuction;
    public Text skillStat;
    public Text skillDescription;
    public Text initMotify;
    public Text embodiMotify;
    public Text moveMotify;

    private MotifyInfo initInfo;
    private MotifyInfo embodiInfo;
    private MotifyInfo moveInfo;

    public static UI_SkillTip Instance => instance;
    public MotifyInfo InitInfo { get { return initInfo == null ? null : initInfo; } set { initInfo = value; } }
    public MotifyInfo EmbodiInfo { get { return embodiInfo == null ? null : embodiInfo; } set { embodiInfo = value; } }
    public MotifyInfo MoveInfo { get { return moveInfo == null ? null : moveInfo; } set { moveInfo = value; } }

    void Start()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void SetToolTip(SkillInfo skillInfo)
    {
        Texture2D texture = Managers.Resource.Load<Texture2D>(skillInfo.icon);
        icon.sprite = Managers.UI.TextureToSprite(texture);

        skillName.text = string.Format($"{skillInfo.name}");
        skillStat.text = string.Format($"¸¶³ª : {skillInfo.mana}");
        skillFuction.text = string.Format($"{skillInfo.function}");
        skillDescription.text = string.Format($"{skillInfo.description}");

        string initName = InitInfo == null ? Managers.Data.MotifyDict[100].name : initInfo.name;
        string embodiName = EmbodiInfo == null ? Managers.Data.MotifyDict[150].name : embodiInfo.name;
        string moveName = MoveInfo == null ? Managers.Data.MotifyDict[200].name : moveInfo.name;

        initMotify.text = string.Format($"{initName}");
        embodiMotify.text = string.Format($"{embodiName}");
        moveMotify.text = string.Format($"{moveName}");
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
