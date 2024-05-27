using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class FetchKeyQuestStep : QuestStep
{
    public int keysCollected = 0;//Number of keys collected
    public int keysToCollect = 1;//Number of keys required to complete the quest step

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(); //Update the state of the quest step when it starts
    }

    private void OnEnable()
    {
        //Subscribe to the event for when the key is collected
        GameEventsManager.instance.miscEvents.onKeyCollected += keyCollected;
    }

    private void OnDisable()
    {
        //Unsubscribe from the event for when a key is collected
        GameEventsManager.instance.miscEvents.onKeyCollected -= keyCollected;
    }

    private void keyCollected()
    {
        //Increment the key collected and update the state
        if (keysCollected < keysToCollect)
        {
            keysCollected++;
            UpdateState();
        }

        //If key has been collected, finish the quest step
        if (keysCollected >= keysToCollect)
        {
            FinishQuestStep();
        }
    }

    //Update the state of the quest step based on the number of Enemies slain
    private void UpdateState()
    {
        string state = keysCollected.ToString();
        string status = "Collected " + keysCollected + " / " + keysToCollect + " key.";
        ChangeState(state, status); //Update the quest step state
    }

    //Set the quest step state when initializing based on the provided state
    protected override void SetQuestStepState(string state)
    {
        this.keysCollected = System.Int32.Parse(state); //Parse the state string to get the number of enemies slain
        UpdateState(); //Update the state of the quest step
    }
}

