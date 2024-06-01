using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction, IItemBuyAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        [SerializeField]
        public int price;

        [SerializeField]
        public int damage;

        [SerializeField]
        public bool weaponTypePhysic;

        [SerializeField]
        public bool weaponTypeMagic;
        
        private WeaponParent weaponParent;

        private SpellBook spellBook;

        int IShopItem.Price => price;

        public Sprite Icon => throw new System.NotImplementedException();

        public void Purchase()
        {
            Debug.Log($"{Name} sold for {price} gold!");
            // Call a method in your InventoryController to remove the item from inventory
        }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParametersList : itemState);
                return true;
            }
            return false;
        }

        public void SetWeapon(AgentWeapon weapon)
        {
            if (weapon != null)
            {
                if (weaponTypeMagic == true)
                {
                    weaponParent.DetectColliders();
                }
                else if (weaponTypePhysic == true)
                {
                    spellBook.Update();
                }
            }
            else {
                Debug.Log("null");
            }
        }
    }
}