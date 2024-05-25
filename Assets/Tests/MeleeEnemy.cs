using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Idle, Attacking }

public class MeleeEnemy
{
    // Attack cooldown settings
    public float attackCooldown = 2.0f;
    public float currentCooldown = 0.0f;

    // Current enemy state
    public EnemyState currentState = EnemyState.Idle;

    // Execute attack logic
    public void Attack()
    {
        if (currentCooldown <= 0)
        {
            // Add code here to perform attack
            currentCooldown = attackCooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    // Update method to simulate enemy behavior
    public void Update()
    {
        // Simulate cooldown reduction
        currentCooldown -= Time.deltaTime;

        // Update enemy state based on cooldown
        if (currentCooldown <= 0)
        {
            currentState = EnemyState.Attacking;
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }
}