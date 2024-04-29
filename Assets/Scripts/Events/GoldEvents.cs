using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GoldEvents class responsible for managing gold-related events
public class GoldEvents
{
    //Event for when gold is gained
    public event GoldEventHandler onGoldGained;

    //Event for when gold amount changes
    public event GoldEventHandler onGoldChange;

    //Method to invoke the onGoldGained event with the given goldAmount
    public void GoldGained(int goldAmount)
    {
        onGoldGained?.Invoke(goldAmount);
    }

    //Method to invoke the onGoldChange event with the given currentGold
    public void GoldChange(int currentGold)
    {
        onGoldChange?.Invoke(currentGold);
    }
}
