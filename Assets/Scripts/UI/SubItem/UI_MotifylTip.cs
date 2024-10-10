using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_MotifylTip : MonoBehaviour
{
    static UI_MotifylTip        instance;
    public CanvasGroup          group;
    public RectTransform        parentRect;
    public Image                motifyIcon;
    public Text                 motifyName;
    public Text                 motifyFuction;
    public Text                 motifyStat;
    public Text                 motifyDescription;

    public static UI_MotifylTip Instance => instance;
    private void Start()
    {
        instance = this;
        parentRect = transform.parent.GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    public void SetToolTip(Motify motify)
    {
        Texture2D texture = Managers.Resource.Load<Texture2D>(motify.info.icon);
        motifyIcon.sprite = Managers.UI.TextureToSprite(texture);

        motifyName.text =        string.Format($"{motify.info.name}");
        motifyStat.text =        string.Format($"{motify.info.mana}");
        motifyFuction.text =     string.Format($"{motify.info.function}");
        motifyDescription.text = string.Format($"{motify.info.description}");        
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
