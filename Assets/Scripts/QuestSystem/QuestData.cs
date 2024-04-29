using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public QuestState state; //State of the quest
    public int questStepIndex; //Index of the current quest step
    public QuestStepState[] questStepStates; //Array to store states of each quest step

    //Constructor to initialize quest data
    public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates)
    {
        this.state = state;
        this.questStepIndex = questStepIndex;
        this.questStepStates = questStepStates;
    }
}