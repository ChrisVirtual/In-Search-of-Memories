using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class representing a step in a quest
public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false; //Indicates if the quest step is finished
    private string questId; //The ID of the quest this step belongs to
    private int stepIndex; //The index of this step within the quest

    //Initializes the quest step with the provided quest ID, step index, and current state
    public void InitialiseQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;

        //Set the quest step state if it's not null or empty
        if (!string.IsNullOrEmpty(questStepState))
        {
            SetQuestStepState(questStepState);
        }
    }

    //Marks the quest step as finished and advances the quest
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            Destroy(this.gameObject); //Destroy the quest step object
        }
    }

    //Changes the state of the quest step and notifies the quest manager
    protected void ChangeState(string newState, string newStatus)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState, newStatus));
    }

    //Abstract method to be implemented by subclasses to set the quest step state
    protected abstract void SetQuestStepState(string state);
}