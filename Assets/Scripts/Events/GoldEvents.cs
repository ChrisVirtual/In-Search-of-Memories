using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoldEvents
{
    public event GoldEventHandler onGoldGained;
    public event GoldEventHandler onGoldChange;

    public void GoldGained(int goldAmount)
    {
        onGoldGained?.Invoke(goldAmount);
    }

    public void GoldChange(int currentGold)
    {
        onGoldChange?.Invoke(currentGold);
    }
}

