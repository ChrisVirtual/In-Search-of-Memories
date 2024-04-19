using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{ 
    public QuestInfoSO info;

    public QuestState state;

    private int currentQuestStepIndex;

    private QuestStepState[] questStepStates;
    public Quest(QuestInfoSO info)
    {
        this.info = info;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for(int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();

        }
    }

    public Quest (QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;
        // if the quest step states and prefabs are different lengths,
        // something has changed during development and the saved data is out of sync.

        if (this.questStepStates.Length != this.info.questStepPrefabs.Length)
        {
            Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                + "of different lengths. This indicates something changed "
                + "with the QuestInfo and the saved data is now out of sync. "
                + "Reset your data - as this might cause issues. QuestId: " + this.info.id);
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if(questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitialiseQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }
    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else 
        {
            Debug.LogWarning("Step index was out of range meaning that there is no current quest step QuestID= " + info.id + ", stepIndex= " + currentQuestStepIndex);
        }
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("Tried to access quest step data, but step index was out of range quest ID: " + info.id +", step index = " + stepIndex);
        }
       

    }

    public string GetFullStatusText()
    {
        string fullStatus = "";

        if (state == QuestState.REQUIREMENTS_NOT_MET)
        {
            fullStatus = "Requirements are not yet met to start this quest.";
        }
        else if (state == QuestState.CAN_START)
        {
            fullStatus = "This quest can be started!";
        }
        else
        {
            // display all previous quests with strikethroughs
            for (int i = 0; i < currentQuestStepIndex; i++)
            {
                fullStatus += "<s>" + questStepStates[i].status + "</s>\n";
            }
            // display the current step, if it exists
            if (CurrentStepExists())
            {
                fullStatus += questStepStates[currentQuestStepIndex].status;
            }
            // when the quest is completed or turned in
            if (state == QuestState.CAN_FINISH)
            {
                fullStatus += "The quest is ready to be turned in.";
            }
            else if (state == QuestState.FINISHED)
            {
                fullStatus += "The quest has been completed!";
            }
        }

        return fullStatus;
    }
}
   