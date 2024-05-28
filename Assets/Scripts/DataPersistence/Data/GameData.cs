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
    public ItemSO InventoryItem;
    public InventoryController InventoryController;
    public Vector3 playerPosition;
    public float playerHealth;
    public int currentMana;
    public int currentExp;
    public int playerLevel;
    public int statPoints;

    // The constructor holds initial values when there's no data saved to load. 
    public GameData()
    {
        coinsCollected = new Dictionary<string, bool>();
        this.gold = 0;
        playerPosition = Vector3.zero;
        
    }
}
