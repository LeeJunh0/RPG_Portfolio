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

    private void Awake()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void SetToolTip(SkillInfo skillInfo)
    {
        SkillInventory skillInventory = Managers.Game.GetPlayer().GetComponent<SkillInventory>();
        MotifyInfo[] motifys = skillInventory.skillMotifies[skillInfo];

        Texture2D texture = Managers.Resource.Load<Texture2D>(skillInfo.icon);
        icon.sprite = Managers.UI.TextureToSprite(texture);

        skillName.text = string.Format($"{skillInfo.name}");
        skillStat.text = string.Format($"¸¶³ª : {skillInfo.mana}");
        skillFuction.text = string.Format($"{skillInfo.function}");
        skillDescription.text = string.Format($"{skillInfo.description}");

        string initName = motifys[0] == null ? Managers.Data.MotifyDict[100].name : motifys[0].name;
        string embodiName = motifys[1] == null ? Managers.Data.MotifyDict[150].name : motifys[1].name;
        string moveName = motifys[2] == null ? Managers.Data.MotifyDict[200].name : motifys[2].name;

        initMotify.text = string.Format($"{initName}");
        embodiMotify.text = string.Format($"{embodiName}");
        moveMotify.text = string.Format($"{moveName}");
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
