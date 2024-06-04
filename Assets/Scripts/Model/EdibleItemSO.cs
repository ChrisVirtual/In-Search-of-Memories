using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction, IItemBuyAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();

        public string ActionName => "Consume";

        [SerializeField]
        public int price;

        public Sprite Icon => throw new NotImplementedException();

        [field: SerializeField]
        public AudioClip actionSFX { get; private set;}

        int IShopItem.Price => price;

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }

        public void Purchase()
        {
            Debug.Log($"{Name} sold for {price} gold!");
            // Call a method in your InventoryController to remove the item from inventory
        }
    }

    public interface IDestroyableItem
    {}

    public interface IShopItem
    {
        string name { get; }
        int Price { get; }
        Sprite Icon { get; }
        void Purchase();
    }

    public interface IItemAction 
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get;}
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    public interface IItemBuyAction : IShopItem
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get;}
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}