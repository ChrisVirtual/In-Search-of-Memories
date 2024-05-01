using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    
    [SerializeField]
    private bool isDead = false;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue; // set current heath as the heath value given to the GameObject
        maxHealth = healthValue; // set max health of the GameObject
        isDead = false; // Mark the gameObject as not dead
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead) // If already dead, do nothing
            return;
        if (sender.layer == gameObject.layer)
            return; // Don't take damage from objects on the same layer
        
        currentHealth -= amount; // Decrease health by the damage amount

        if (currentHealth > 0) 
        {
            OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
        }
        else
        {
            OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
            isDead = true; // Mark as dead
            Destroy(gameObject); // Destroy the GameObject
        }
    }
}
