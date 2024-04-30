using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Serializable class representing the state of a quest step
[System.Serializable]
public class QuestStepState
{
    public string state; //Current state of the quest step
    public string status; //Additional status information about the quest step

    //Constructor with parameters to initialize the state and status
    public QuestStepState(string state, string status)
    {
        this.state = state;
        this.status = status;
    }

    //Default constructor initializing state and status to empty strings
    public QuestStepState()
    {
        this.state = "";
        this.status = "";
    }
}
