using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

//Represents a button in the quest log
public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    public Button button { get; private set; } //Reference to the button component
    private TextMeshProUGUI buttonText; //Reference to the text component of the button
    private UnityAction onSelectAction; //Action to be invoked when the button is selected

    //Initialize the button with a display name and select action
    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.button = this.GetComponent<Button>(); //Get the button component
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>(); //Get the text component
        this.buttonText.text = displayName; //Set the display name
        this.onSelectAction = selectAction; //Set the select action
    }

    //Invoked when the button is selected
    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction(); //Invoke the select action
    }

    //Set the color of the button text based on the quest state
    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
            case QuestState.CAN_START:
                buttonText.color = Color.red; //Set color to red
                break;
            case QuestState.IN_PROGRESS:
            case QuestState.CAN_FINISH:
                buttonText.color = Color.yellow; //Set color to yellow
                break;
            case QuestState.FINISHED:
                buttonText.color = Color.green; //Set color to green
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement: " + state); //Log a warning for unrecognized state
                break;
        }
    }
}