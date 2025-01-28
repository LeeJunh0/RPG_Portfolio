using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvenTip : MonoBehaviour
{
    private static UI_InvenTip instance;

    public CanvasGroup group;
    public RectTransform parentRect;
    public Image icon;
    public Text itemName;
    public Text itemStat;
    public Text itemDescription;
    public Text itemSell;

    public static UI_InvenTip Instance => instance;

    void Start()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }
    public void SetToolTip(Iteminfo item)
    {       
        Texture2D texture = Managers.Resource.Load<Texture2D>(item.uiInfo.icon);
        icon.sprite = Managers.UI.TextureToSprite(texture);

        itemName.text = string.Format($"{item.uiInfo.name}");        
        itemDescription.text = string.Format($"{item.uiInfo.description}");
        itemSell.text = string.Format($"{item.gold}");

        if (item.hp > 0)
            itemStat.text = string.Format($"체력 : +{item.hp}");
        else if (item.att > 0)
            itemStat.text = string.Format($"공격력 : +{item.att}");
        else
            itemStat.text = " ";
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
