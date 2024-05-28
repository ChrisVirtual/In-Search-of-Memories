using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Shop;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem inventoryItemUIPrefab;
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private InventoryDescription inventoryDescription;
        [SerializeField]
        private MouseFollower mouseFollower;
    
        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();
    
        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested;

        [SerializeField]
        private ItemActionPanel actionPanel;
        internal NPCController inventoryController;

        private void Awake()
        {
           Hide(); 
           mouseFollower.Toggle(false);
           inventoryDescription.RestDescription();
        }
    
        public void InitializeInventoryUI(int inventorysize)
        {       
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(inventoryItemUIPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }
    
        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            inventoryDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }
    
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }
    
        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnItemActionRequested?.Invoke(index);
        }
    
        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) 
                return;
            OnDescriptionRequested?.Invoke(index);
        }
    
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }
    
        public void ResetSelection()
        {
            inventoryDescription.RestDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }
    
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }
    
        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
        }

        internal void ResetAllItems()
        {
            foreach(var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}
