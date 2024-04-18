using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents : MonoBehaviour
{
   
        // Event handlers
        public event MiscEventHandler onCoinCollected;

        // Method to trigger event
        public void CoinCollected()
        {
            onCoinCollected?.Invoke();
        }
    
}
