using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    private Transform weaponTransform;
    private Vector3 scale;
    public GameObject spellPrefab;  // Prefab of the spell to instantiate
    public Transform spawnPoint;    // Point from where the spell will be instantiated
    public KeyCode castSpellKey = KeyCode.Mouse0; // Left mouse button by default
    public float cooldownTime = 2f; // Cooldown time in seconds
    private float nextCastTime = 0f;

    public void Awake()
    {
        weaponTransform = transform;
        scale = weaponTransform.localScale;
    }

    void Update()
    {
        // Rotate the weapon towards the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateTowards(mousePosition);

        // Cast the spell if the key is pressed and the cooldown has elapsed
        if (Input.GetKeyDown(castSpellKey) && Time.time >= nextCastTime)
        {
            CastSpell(mousePosition);
            nextCastTime = Time.time + cooldownTime;
        }
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
    public void CastSpell(Vector3 targetPosition)
    {
        if (spellPrefab != null && spawnPoint != null)
        {
            GameObject spell = Instantiate(spellPrefab, spawnPoint.position, Quaternion.identity);
            spell.GetComponent<Spell>().Initialize(targetPosition);  // Assuming the spell has an Initialize method
        }
        else
        {
            Debug.LogWarning("SpellPrefab or SpawnPoint is not assigned.");
        }
    }
}