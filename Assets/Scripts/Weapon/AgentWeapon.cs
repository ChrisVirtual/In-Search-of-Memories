using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private GameObject weaponObj;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;
    
    private SpriteRenderer weaponSpriteRenderer;

    private void Awake()
    {
        // Find the child object named "weapon" and get its SpriteRenderer component
        
        if (weaponObj != null)
        {
            weaponSpriteRenderer = weaponObj.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Weapon child object not found.");
        }
    }

    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }

        this.weapon = weaponItemSO;
        
        // Set the sprite of the weapon child object's SpriteRenderer
        if (weaponSpriteRenderer != null)
        {
            weaponSpriteRenderer.sprite = weaponItemSO.ItemImage;
        }
        else
        {
            Debug.LogError("Weapon SpriteRenderer not found.");
        }
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue,
                };
            }
        }
    }
}
