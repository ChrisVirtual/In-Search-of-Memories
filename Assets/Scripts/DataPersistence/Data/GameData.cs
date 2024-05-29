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
    public InventoryPage inventoryUI;
    public InventorySO inventoryData;
    public List<InventoryItem> initialItems;

    // Audio fields
    public AudioClip dropClip;
    public AudioSource audioSource;
     
    // Serialized fields for attached objects
    public List<string> inventoryUIAttachedObjects; // Save names or unique identifiers of attached objects
    
    // The constructor holds initial values when there's no data saved to load.
    public GameData()
    {
        coinsCollected = new Dictionary<string, bool>();
        this.gold = 0;
        inventoryState = new List<InventoryItem>();
        playerPosition = Vector3.zero;
        inventoryUIAttachedObjects = new List<string>();
        inventoryUI = new InventoryPage();
    }
}
