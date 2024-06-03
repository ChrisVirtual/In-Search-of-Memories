using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model; 

public class SpellBook : MonoBehaviour
{
    private Transform weaponTransform;
    private Vector3 scale;
    public GameObject spellPrefab;  // Prefab of the spell to instantiate
    public Transform spawnPoint;    // Point from where the spell will be instantiated
    public Vector3 targetPosition;
    public KeyCode castSpellKey = KeyCode.Mouse0; // Left mouse button by default
    public float cooldownTime = 2f; // Cooldown time in seconds
    private float nextCastTime = 0f;

    // Reference to the EquippableItemSO instance associated with the spellbook
    public EquippableItemSO equippableItem;

    public void Awake()
    {
        weaponTransform = transform;
        scale = weaponTransform.localScale;
    }

    public void Update()
    {
        // Rotate the weapon towards the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateTowards(mousePosition);

        // Check if the spellbook is equipped before allowing casting
        if (IsEquipped() && Input.GetKeyDown(castSpellKey) && Time.time >= nextCastTime)
        {
            CastSpell(mousePosition, equippableItem.damage);
            nextCastTime = Time.time + cooldownTime;
        }
    }

    private bool IsEquipped()
    {
        // The spellbook is considered equipped if the equippableItem reference is not null
        return equippableItem != null;
    }

    // Rotate the weapon towards a target position
    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - weaponTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Flip the weapon vertically based on the direction
        if (direction.x < 0)
        {
            weaponTransform.localScale = new Vector3(scale.x, -scale.y, scale.z);
        }
        else
        {
            weaponTransform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    // Cast the spell towards the target position
    public void CastSpell(Vector3 targetPosition, int damageValue)
    {
        this.targetPosition = targetPosition;
        if (spellPrefab != null && spawnPoint != null)
        {
            GameObject spellObject = Instantiate(spellPrefab, spawnPoint.position, Quaternion.identity);
            Spell spell = spellObject.GetComponent<Spell>();
            if (spell != null)
            {
                // Pass damage value to the spell
                spell.Initialize(targetPosition, damageValue); // Pass target position and damage
            }
            else
            {
                Debug.LogWarning("Spell component not found on the instantiated spell object.");
            }
        }
        else
        {
            Debug.LogWarning("SpellPrefab or SpawnPoint is not assigned.");
        }
    }

    public void ResetState()
    {
        nextCastTime = 0f; // Reset cooldown timer or other state-specific attributes
    }
}