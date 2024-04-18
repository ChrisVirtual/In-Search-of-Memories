using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0;
    private int coinsToComplete = 5;


    private void OnEnable()
    {
       GameEventsManager.instance.miscEvents.onCoinCollected += coinCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= coinCollected;
    }

    private void coinCollected()
    {
        if (coinsCollected < coinsToComplete)
        {
            coinsCollected++;
        }

        if(coinsCollected >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }
}
