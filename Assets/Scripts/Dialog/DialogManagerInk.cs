using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using System;

public class DialogManagerInk : MonoBehaviour
{
    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    private bool waitingForInput;
    private Story currentStory;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    public bool dialogIsPlaying { get; private set; }
    public static DialogManagerInk instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one dialog manager");
        }
        instance = this;
    }

    void Start()
    {

        dialogIsPlaying = false;
        dialogPanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

  public void EnterDialogMode(TextAsset inkJSON)
{
    currentStory = new Story(inkJSON.text);
    dialogIsPlaying = true;
    dialogPanel.SetActive(true);
    waitingForInput = true;
        currentStory.BindExternalFunction("levelCheck", (int requiredLevel) =>
        {
            bool levelMet = true;
            if (PlayerStats.instance.currentLevel < requiredLevel)
            {
                levelMet = false;
            }

            return levelMet;
        });
        currentStory.BindExternalFunction("checkCanStart", (int requiredLevel, string questId) =>
        {
            bool canStart = true;
            if (PlayerStats.instance.currentLevel < requiredLevel)
            {
                canStart = false;
            }

            // Find the QuestPoint instance with the corresponding questId
            QuestPoint questPoint = FindObjectOfType<QuestPoint>();
            if (questPoint != null && questPoint.questId == questId)
            {
                if (questPoint.GetCurrentQuestState() != QuestState.CAN_START)
                {
                    canStart = false;
                }
            }
            else
            {
                Debug.LogError("QuestPoint with questId " + questId + " not found in the scene.");
            }

            return canStart;
        });
        currentStory.BindExternalFunction("completed", (string questId) =>
        {
            bool completed = false;

            // Find the QuestPoint instance with the corresponding questId
            QuestPoint questPoint = FindObjectOfType<QuestPoint>();
            if (questPoint != null && questPoint.questId == questId)
            {
                if (questPoint.GetCurrentQuestState() == QuestState.FINISHED)
                {
                    
                        completed = true;
                }
            }
         

            return completed;
        });
        currentStory.BindExternalFunction("startQuest", (string questId) =>
    {
        Debug.Log("Inkle output: " + questId);
        GameEventsManager.instance.questEvents.StartQuest(questId);
        
    });

    ContinueStory();
}

    private void ExitDialogMode()
    {
        dialogIsPlaying = false;
        dialogPanel.SetActive(false);
        dialogText.text = "";
    }

    void Update()
    {
        if (!dialogIsPlaying)
        {
            return;
        }

        if (currentStory.currentChoices.Count == 0 && waitingForInput && Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
        else if (!waitingForInput && !currentStory.canContinue)
        {
            ExitDialogMode();
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue();
            waitingForInput = true;

            DisplayChoices();
        }
        else
        {
            waitingForInput = false;
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support");
        }

        int index = 0;

        //Initialises choices to the amount of choices for this line of dialog
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //Hide leftover null choices
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        
        StartCoroutine(selectFirstChoice());
    }

    private IEnumerator selectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    public void MakeChoice(int choiceIndex)
    {
        
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

}
