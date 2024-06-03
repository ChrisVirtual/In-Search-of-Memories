using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO currentWeapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private GameObject physicalWeaponObj;

    [SerializeField]
    private GameObject magicalWeaponObj;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    private SpriteRenderer physicalWeaponSpriteRenderer;
    private SpriteRenderer magicalWeaponSpriteRenderer;
    private SpellBook spellBook;

    private void Awake()
    {
        // Find the SpriteRenderer and SpellBook components on the weaponObj
        if (physicalWeaponObj != null)
        {
            physicalWeaponSpriteRenderer = physicalWeaponObj.GetComponent<SpriteRenderer>();
            physicalWeaponObj.SetActive(false); // Ensure it's inactive by default
        }
        else
        {
            Debug.LogError("Physical weapon child object not found.");
        }

        if (magicalWeaponObj != null)
        {
            magicalWeaponSpriteRenderer = magicalWeaponObj.GetComponent<SpriteRenderer>();
            spellBook = magicalWeaponObj.GetComponent<SpellBook>();
            magicalWeaponObj.SetActive(false); // Ensure it's inactive by default
            if (spellBook == null)
            {
                Debug.LogError("SpellBook component not found on magical weapon object.");
            }
        }
        else
        {
            Debug.LogError("Magical weapon child object not found.");
        }
    }

    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        // If a weapon is already equipped, add it back to the inventory
        if (currentWeapon != null)
        {
            inventoryData.AddItem(currentWeapon, 1, itemCurrentState);
        }

        // Hide all weapons by default
        physicalWeaponObj.SetActive(false);
        magicalWeaponObj.SetActive(false);

        this.currentWeapon = weaponItemSO;

        // Determine which weapon to activate based on the type
        if (weaponItemSO.weaponTypePhysic)
        {
            Debug.Log("Equipping physical weapon.");
            physicalWeaponObj.SetActive(true);
            if (physicalWeaponSpriteRenderer != null)
            {
                physicalWeaponSpriteRenderer.sprite = weaponItemSO.ItemImage;
                Debug.Log("Physical weapon sprite set.");
            }
            else
            {
                Debug.LogError("Physical weapon SpriteRenderer not found.");
            }
        }
        else if (weaponItemSO.weaponTypeMagic)
        {
            Debug.Log("Equipping magical weapon.");
            magicalWeaponObj.SetActive(true);
            if (magicalWeaponSpriteRenderer != null)
            {
                magicalWeaponSpriteRenderer.sprite = weaponItemSO.ItemImage;
                Debug.Log("Magical weapon sprite set.");
            }

            // Set the equippableItem reference in the SpellBook
            if (spellBook != null)
            {
                spellBook.equippableItem = weaponItemSO;
                spellBook.ResetState(); // Reset the state of the SpellBook
            }
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