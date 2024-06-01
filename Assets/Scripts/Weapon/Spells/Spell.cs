using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;
    public int damage;

    public EquippableItemSO equippableItemSO;

    public Collider2D other;

    // Initialize the spell with a target position
    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        Destroy(gameObject, 5f); // Destroy the spell after 5 seconds
    }

    public void Update()
    {
        // Move the spell towards the target position
        if (targetPosition != null)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D()
    {
        this.damage = equippableItemSO.damage;
        // Check if the collider's GameObject is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Ignore collisions with the player and return
            return;
        }

        // Check if the collider is a CapsuleCollider2D
        CapsuleCollider2D capsuleCollider = other as CapsuleCollider2D;
        if (capsuleCollider != null)
        {
            // Apply damage to the enemy
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.GetHit(damage, gameObject); // Pass the damage and the sender GameObject
                Debug.Log("Spell projectile hit an enemy!");
            }

            // Destroy the spell regardless of whether it hit a target
            Destroy(gameObject);
        }
    }
}
