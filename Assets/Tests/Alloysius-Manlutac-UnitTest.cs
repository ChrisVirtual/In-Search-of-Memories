using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    private MeleeEnemy meleeEnemy;

    [SetUp]
    public void Setup()
    {
        // Initialize a new instance of MeleeEnemy for each test
        meleeEnemy = new MeleeEnemy();
    }

    [Test]
    public void MeleeEnemy_AttackCooldown_NotZero()
    {
        // Arrange: Set up the initial state

        // Act: Call the method or property to be tested
        float attackCooldown = meleeEnemy.attackCooldown;

        // Assert: Verify the expected behavior
        Assert.AreNotEqual(0.0f, attackCooldown);
    }

    [Test]
    public void MeleeEnemy_CurrentCooldown_DecreasesOverTime()
    {
        // Arrange
        float initialCooldown = meleeEnemy.currentCooldown;

        // Act
        meleeEnemy.Update();

        // Assert
        Assert.Less(meleeEnemy.currentCooldown, initialCooldown);
    }

    [Test]
    public void MeleeEnemy_Attack_CooldownResets()
    {
        // Arrange
        meleeEnemy.currentCooldown = 0.0f;

        // Act
        meleeEnemy.Attack();

        // Assert
        Assert.AreEqual(meleeEnemy.attackCooldown, meleeEnemy.currentCooldown);
    }

    [Test]
    public void MeleeEnemy_Update_SwitchesState()
    {
        // Arrange
        float initialCooldown = meleeEnemy.currentCooldown;
        meleeEnemy.attackCooldown = 2.0f; // Set a cooldown for testing

        // Act
        meleeEnemy.Update();

        // Assert
        if (initialCooldown <= 0)
        {
            Assert.AreEqual(EnemyState.Attacking, meleeEnemy.currentState);
        }
        else
        {
            Assert.AreEqual(EnemyState.Idle, meleeEnemy.currentState);
        }
    }
}
