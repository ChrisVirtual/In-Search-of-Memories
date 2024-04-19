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
    
}