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
        this.InventoryItem = null;
        playerPosition = Vector3.zero;
        playerHealth = 10f; // Set initial health value here
        currentMana = 0; // Set initial mana value here
        currentExp = 0; // Set initial exp value here
        playerLevel = 1; // Set initial level value here
        statPoints = 0; // Set initial stat points value here
    }
}
