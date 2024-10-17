using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_MotifyTip : MonoBehaviour
{
    static UI_MotifyTip         instance;
    public CanvasGroup          group;
    public RectTransform        parentRect;
    public Image                motifyIcon;
    public Text                 motifyName;
    public Text                 motifyFuction;
    public Text                 motifyStat;
    public Text                 motifyDescription;

    public static UI_MotifyTip Instance => instance;
    private void Start()
    {
        instance = this;
        parentRect = transform.parent.GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    public void SetToolTip(MotifyInfo motifyInfo)
    {
        Texture2D texture = Managers.Resource.Load<Texture2D>(motifyInfo.icon);
        motifyIcon.sprite = Managers.UI.TextureToSprite(texture);

        motifyName.text = string.Format($"{motifyInfo.name}");
        motifyStat.text = string.Format($"¸¶³ª : {motifyInfo.mana}");
        motifyFuction.text = string.Format($"{motifyInfo.function}");
        motifyDescription.text = string.Format($"{motifyInfo.description}");
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
