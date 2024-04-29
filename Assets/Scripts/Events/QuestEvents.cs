using System;

//QuestEvents class responsible for managing quest-related events
public class QuestEvents
{
    //Event for when a quest is started
    public event Action<string> onStartQuest;

    //Method to invoke the onStartQuest event with the given quest ID
    public void StartQuest(string id)
    {
        if (onStartQuest != null)
        {
            onStartQuest(id);
        }
    }

    //Event for when a quest advances
    public event Action<string> onAdvanceQuest;

    //Method to invoke the onAdvanceQuest event with the given quest ID
    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    //Event for when a quest is finished
    public event Action<string> onFinishQuest;

    //Method to invoke the onFinishQuest event with the given quest ID
    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    //Event for when the state of a quest changes
    public event Action<Quest> onQuestStateChange;

    //Method to invoke the onQuestStateChange event with the given quest
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }

    //Event for when the state of a quest step changes
    public event Action<string, int, QuestStepState> onQuestStepStateChange;

    //Method to invoke the onQuestStepStateChange event with the given parameters
    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null)
        {
            onQuestStepStateChange(id, stepIndex, questStepState);
        }
    }
}