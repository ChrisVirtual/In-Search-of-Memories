using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Quest step for talking to an NPC
[RequireComponent(typeof(CircleCollider2D))]
public class TalkToNPC : QuestStep
{
    bool inRange = false; //Flag indicating if the player is in range to talk to the NPC

    
    protected override void SetQuestStepState(string state)
    {
        //This method is not implemented for this quest step since talking to an NPC does not involve specific state changes
    }

    //Called when a collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true; //Set inRange flag to true when the player enters the trigger zone
        }
    }

    //Update is called once per frame
    public void Update()
    {
        input(); //Check for player input
    }

    //Method to handle player input
    private void input()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E)) //Check if the player presses the interaction key (E)
            {
                FinishQuestStep(); //Finish the quest step if the player interacts with the NPC
            }
        }
    }

    //Called when a collider exits the trigger zone
    private void OnTriggerExit2D(Collider2D collider)
    {
        inRange = false; //Set inRange flag to false when the player exits the trigger zone
    }
}