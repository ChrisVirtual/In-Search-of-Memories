using Inventory;
using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Dictionary<string, bool> coinsCollected;
    public int gold;
    public List<InventoryItem> inventoryState; // List to store inventory items
    public Vector3 playerPosition;

    public float playerHealth;
    public int currentMana;
    public int currentExp;
    public int playerLevel;
    public int statPoints;

    // Serialiezed fields from InventoryController
    
    public List<InventoryItem> initialItems;
    
    // The constructor holds initial values when there's no data saved to load.
    public GameData()
    {
        this.coinsCollected = new Dictionary<string, bool>();
        this.gold = 0;
        this.playerPosition = Vector3.zero;
    }
}
