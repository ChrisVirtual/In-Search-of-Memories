using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject requirementsNotMetToStartIcon; // Icon indicating requirements are not met to start the quest
    [SerializeField] private GameObject canStartIcon; // Icon indicating the quest can be started
    [SerializeField] private GameObject requirementsNotMetToFinishIcon; // Icon indicating requirements are not met to finish the quest
    [SerializeField] private GameObject canFinishIcon; // Icon indicating the quest can be finished

    // Method to set the state of the quest icon based on the new state
    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        //Set all icons to inactive
        requirementsNotMetToStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requirementsNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        //Set the appropriate icon to active based on the new state
        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { requirementsNotMetToStartIcon.SetActive(true); }
                break;
            case QuestState.CAN_START:
                if (startPoint) { canStartIcon.SetActive(true); }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint) { requirementsNotMetToFinishIcon.SetActive(true); }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint) { canFinishIcon.SetActive(true); }
                break;
            case QuestState.FINISHED:
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                break;
        }
    }
}
