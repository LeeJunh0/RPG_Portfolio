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
    public static UI_SkillTip Instance => instance;
    
    void Start()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void SetToolTip(Skill skill)
    {
        Texture2D texture = Managers.Resource.Load<Texture2D>(skill.skillData.icon);
        icon.sprite = Managers.UI.TextureToSprite(texture);

        skillName.text = string.Format($"{skill.skillData.name}");
        skillStat.text = string.Format($"{skill.skillData.mana}");
        skillFuction.text = string.Format($"{skill.skillData.function}");
        skillDescription.text = string.Format($"{skill.skillData.description}");
    }
}
