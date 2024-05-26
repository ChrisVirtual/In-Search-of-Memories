using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Dictionary<string, bool> coinsCollected;
    public int gold;
    public ItemSO InventoryItem;
    public Vector3 playerPosition;

    // The constructor holds initial values when there's no data saved to load. 
    public GameData()
    {
        coinsCollected = new Dictionary<string, bool>();
        this.gold = 0;
        this.InventoryItem = null;
        playerPosition = Vector3.zero;
    }
}
