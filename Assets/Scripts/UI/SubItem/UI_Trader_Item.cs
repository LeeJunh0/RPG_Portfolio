using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trader_Item : UIPopup
{
    enum ItemButtons
    {
        UI_Trader_Sell,
        UI_Trader_Buy
    }

    enum ItemImage
    {
        UI_Trader_Item_Slot
    }

    enum ItemTexts
    {
        UI_Trader_Item_Name,
        UI_Trader_Item_Sell
    }

    public override void Init()
    {
        BindButton(typeof(ItemButtons));
        BindImage(typeof(ItemImage));
        BindText(typeof(ItemTexts));
    }

    public void ItemInit(Iteminfo info)
    {
        Image slot = GetImage((int)ItemImage.UI_Trader_Item_Slot);
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);

        GetText((int)ItemTexts.UI_Trader_Item_Name).text = info.uiInfo.name;
        GetText((int)ItemTexts.UI_Trader_Item_Sell).text = string.Format("${0}", info.gold);

        GetButton((int)ItemButtons.UI_Trader_Buy).gameObject.BindEvent(evt => { ItemBuy(info); });
        GetButton((int)ItemButtons.UI_Trader_Sell).gameObject.BindEvent(evt => { ItemSell(info); });
    }

    public void ItemBuy(Iteminfo item)
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        if(playerStat.Gold - item.gold < 0)
        {
            Debug.Log("화폐 부족");
            return;
        }

        playerStat.Gold -= item.gold;
        Managers.Inventory.AddItem(new Iteminfo(item));
        Debug.Log($"{item.uiInfo.name} 구매성공");
    }

    public void ItemSell(Iteminfo item)
    {
        bool isCheck = Managers.Inventory.RemoveItem(item);
        if (isCheck == false)
        {
            Debug.Log("아이템이 없는데 팔려고 시도함");
            return;
        }

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.Gold += item.gold;
        Debug.Log($"{item.uiInfo.name} 판매 {item.gold} +");
    }
}
