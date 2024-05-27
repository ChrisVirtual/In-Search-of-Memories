using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
        // Event handlers
        public event Action onCoinCollected;

    // Method to trigger event
    public void coinCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected();
        }
    }
         public event Action onEnemyDeath;

    // Method to trigger event
    public void enemyDeath()
    {
        if (onEnemyDeath != null)
        {
            onEnemyDeath();
        }
    }

    public event Action onKeyCollected;

    // Method to trigger event
    public void keyCollected()
    {
        if (onKeyCollected != null)
        {
            onKeyCollected();
        }
    }

}
