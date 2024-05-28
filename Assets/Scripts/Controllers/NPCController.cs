using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shop
{    
    public class NPCController : MonoBehaviour
    {
        [SerializeField]
        private ShopUI ShopUI;
        [SerializeField]
        private InventorySO inventoryData;
        [SerializeField]
        private InventorySO playerInventoryData;
        [SerializeField]
        private InventoryController inventoryController;
        public Item item;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;
        
        [SerializeField] private GoldManager goldManager;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareUI()
        {
            ShopUI.InitializeInventoryUI(inventoryData.Size);
            this.ShopUI.OnDescriptionRequested += HandleDescriptionRequest;        
            this.ShopUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateShopUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateShopUI(Dictionary<int, InventoryItem> inventoryState)
        {
            ShopUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                ShopUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity); 
            }
        }

        public void OpenShop()
        {
            ShopUI.Show();
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IShopItem shopItem = inventoryItem.item as IShopItem;
            if (shopItem != null)
            {
                ShopUI.ShowItemAction(itemIndex);
                ShopUI.AddAction("Buy", () => PurchaseItem(itemIndex));
            }  
        }

        private void PurchaseItem(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IShopItem shopItem = inventoryItem.item as IShopItem;
            if (shopItem != null && goldManager.gold >= shopItem.Price)
            {
                goldManager.gold -= shopItem.Price;
                shopItem.Purchase();
                goldManager.goldEvents.GoldChange(goldManager.gold); 
                // Update the shop UI to reflect the inventory change after the item is sold
                inventoryController.AddItemToPlayerInventory(shopItem);
                inventoryData.RemoveItem(itemIndex, 1);
            }
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
            
            IItemBuyAction itemBuyAction = inventoryItem.item as IItemBuyAction;
            if (itemBuyAction != null)
            {
                itemBuyAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemBuyAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    ShopUI.ResetSelection();
            }
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                ShopUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            ShopUI.UpdateDescription(itemIndex, item.ItemImage ,item.name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " + 
                    $": {inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if(ShopUI.isActiveAndEnabled == false)
                {
                    ShopUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        ShopUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }   
                }
                else
                {
                    ShopUI.Hide();
                }
            }
        }
    }
}
