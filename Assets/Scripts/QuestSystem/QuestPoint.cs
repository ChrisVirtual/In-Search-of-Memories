using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires circle collider2d to be attached to same game object as the script
[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;

    private QuestIcon questIcon;

    private void Awake() 
    {
        Debug.Log("Quest Point Awake");
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
        if (questIcon != null)
        {
            Debug.Log("questIcon is not null");
        }
        else
        {
            Debug.Log("questIcon is null");
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    private void SubmitPressed()
    {
        Debug.Log("SubmitPressed method called in QuestPoint");
        if (!playerIsNear)
        {
            Debug.Log("Says player is not near");
            return;
        }

        // start or finish a quest
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            Debug.Log("Starting quest");
            GameEventsManager.instance.questEvents.StartQuest(questId);
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            Debug.Log("Starting quest");
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
            Debug.Log("Quest state changed to: " + currentQuestState);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
            Debug.Log("Player has entered quest point trigger area");
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
            Debug.Log("Player has entered quest point trigger area");
        }
    }
}
