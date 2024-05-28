using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class ShopItemManager : MonoBehaviour
{
    public List<IShopItem> shopItems;
    public GoldManager goldManager;

    public void BuyItem(IShopItem item)
    {
        if (goldManager.gold >= item.Price)
        {
            goldManager.gold -= item.Price;
            // Add to the player's inventory
            item.Purchase();
            goldManager.goldEvents.GoldChange(goldManager.gold);
        }
        else
        {
            //Caleb will help me 
            Debug.Log("Not enough gold");
        }
    }
}
