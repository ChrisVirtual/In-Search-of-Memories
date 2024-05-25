using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script manages a quest point, which is an area in the game where the player can interact to start or finish a quest
[RequireComponent(typeof(CircleCollider2D))] //Ensures a CircleCollider2D component is attached to the same game object
public class QuestPoint : MonoBehaviour, Interactable
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint; //The quest associated with this point

    [Header("Config")]
    [SerializeField] private bool startPoint = true; //Indicates if this is a starting point for the quest
    [SerializeField] private bool finishPoint = true; //Indicates if this is a finishing point for the quest

    private bool playerIsNear = false; //Tracks if the player is near the quest point trigger area
    public string questId { get; private set; } //The ID of the associated quest
    private QuestState currentQuestState; //The current state of the associated quest

    private QuestIcon questIcon; //Reference to the quest icon component
    
    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        questId = questInfoForPoint.id; //Set the quest ID
        questIcon = GetComponentInChildren<QuestIcon>(); //Get reference to the quest icon component
    }

    //Called when the script instance is enabled
    private void OnEnable()
    {
        //Subscribe to events
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    //Called when the script instance is disabled
    private void OnDisable()
    {
        //Unsubscribe from events
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    //Returns the current state of the associated quest
    public QuestState GetCurrentQuestState()
    {
        return currentQuestState;
    }

    //Called when the player presses the submit button
    private void SubmitPressed()
    {
        if (!playerIsNear) //If the player is not near the quest point trigger area, return
        {
            return;
        }

        //Start or finish a quest based on the current quest state and the type of quest point
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId); //Start the quest
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId); //Finish the quest
        }
    }

    //Called when the state of any quest changes
    private void QuestStateChange(Quest quest)
    {
        //Only update the quest state if it corresponds to the associated quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state; //Update the current quest state
            questIcon.SetState(currentQuestState, startPoint, finishPoint); //Update the quest icon state
        }
    }

    //Called when another collider enters the trigger area of the quest point
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player")) //If the collider belongs to the player
        {
            playerIsNear = true; //Set playerIsNear to true
        }
    }

    //Called when another collider exits the trigger area of the quest point
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player")) //If the collider belongs to the player
        {
            playerIsNear = false; //Set playerIsNear to false
        }
    }

    public void Interact()
    {
        //Start or finish a quest based on the current quest state and the type of quest point
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId); //Start the quest
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId); //Finish the quest
        }
    }
}