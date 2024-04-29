using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

//Quest step for collecting coins
public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0; //Number of coins collected
    private int coinsToComplete = 5; //Number of coins required to complete the quest step

    private void Start()
    {
        UpdateState(); //Update the state of the quest step when it starts
    }

    private void OnEnable()
    {
        //Subscribe to the event for when a coin is collected
        GameEventsManager.instance.miscEvents.onCoinCollected += coinCollected;
    }

    private void OnDisable()
    {
        //Unsubscribe from the event for when a coin is collected
        GameEventsManager.instance.miscEvents.onCoinCollected -= coinCollected;
    }

    private void coinCollected()
    {
        //Increment the coins collected and update the state
        if (coinsCollected < coinsToComplete)
        {
            coinsCollected++;
            UpdateState();
        }

        //If enough coins have been collected, finish the quest step
        if (coinsCollected >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }

    //Update the state of the quest step based on the number of coins collected
    private void UpdateState()
    {
        string state = coinsCollected.ToString();
        string status = "Collected " + coinsCollected + " / " + coinsToComplete + " coins.";
        ChangeState(state, status); //Update the quest step state
    }

    //Set the quest step state when initializing based on the provided state
    protected override void SetQuestStepState(string state)
    {
        this.coinsCollected = System.Int32.Parse(state); //Parse the state string to get the number of coins collected
        UpdateState(); //Update the state of the quest step
    }
}