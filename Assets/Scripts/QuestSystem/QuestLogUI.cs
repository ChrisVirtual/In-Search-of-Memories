using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

// Manages the display and functionality of the quest log UI
public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent; //Parent object of the quest log content
    [SerializeField] private QuestLogScrollingList scrollingList; //Reference to the scrolling list component
    [SerializeField] private TextMeshProUGUI questDisplayNameText; //Text displaying the quest name
    [SerializeField] private TextMeshProUGUI questStatusText; //Text displaying the quest status
    [SerializeField] private TextMeshProUGUI goldRewardsText; //Text displaying the gold rewards
    [SerializeField] private TextMeshProUGUI experienceRewardsText; //Text displaying the experience rewards
    [SerializeField] private TextMeshProUGUI levelRequirementsText; //Text displaying the level requirements
    [SerializeField] private TextMeshProUGUI questRequirementsText; //Text displaying the quest prerequisites

    private Button firstSelectedButton; //Reference to the first selected button in the UI
    public static QuestLogUI instance { get; private set; } //Singleton instance of the quest log UI
    public bool questLogOpen { get; private set; } = false; //Flag indicating whether the quest log is open

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //Set the singleton instance
        }
    }

    private void OnEnable()
    {
        //Subscribe to events
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        //Unsubscribe from events
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    //Handler for quest log toggle button press
    private void QuestLogTogglePressed()
    {
        //Toggle the visibility of the quest log UI
        if (contentParent.activeInHierarchy)
        {
            HideUI();
            Debug.Log("HideUI");
        }
        else
        {
            ShowUI();
            Debug.Log("ShowUI");
        }
    }

    //Show the quest log UI
    private void ShowUI()
    {
        contentParent.SetActive(true); //Activate the content parent
        //Ensure the first selected button is initialized after the content parent is set active
        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select(); //Select the first button
        }
        questLogOpen = true; //Set the flag indicating the quest log is open
        Debug.Log("Quest Log opened");
    }

    //Hide the quest log UI
    private void HideUI()
    {
        contentParent.SetActive(false); //Deactivate the content parent
        EventSystem.current.SetSelectedGameObject(null); //Deselect any selected game object
        questLogOpen = false; //Set the flag indicating the quest log is closed
        Debug.Log("Quest Log closed");
    }

    //Update the quest log UI based on the state of a quest
    private void QuestStateChange(Quest quest)
    {
        //Add a button to the scrolling list if not already added
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => { SetQuestLogInfo(quest); });

        //Initialize the first selected button if not already initialized to ensure it's always the top button
        if (firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }

        //Set the button color based on the quest state
        questLogButton.SetState(quest.state);
    }

    //Set the quest log information based on the selected quest
    private void SetQuestLogInfo(Quest quest)
    {
        //Set the quest name
        questDisplayNameText.text = quest.info.displayName;

        //Set the quest status
        questStatusText.text = quest.GetFullStatusText();

        //Set the level requirements
        levelRequirementsText.text = "Level " + quest.info.levelRequirement;

        //Set the quest prerequisites
        questRequirementsText.text = "";
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            questRequirementsText.text += prerequisiteQuestInfo.displayName + "\n";
        }

        //Set the gold rewards
        goldRewardsText.text = quest.info.goldReward + " Gold";

        //Set the experience rewards
        experienceRewardsText.text = quest.info.experienceReward + " XP";
    }
}