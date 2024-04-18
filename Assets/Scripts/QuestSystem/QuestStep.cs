using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuestStep()
    { 
        if(!isFinished) 
        {
            isFinished = true;

            // To do - Advance quest forward

            Destroy(gameObject);
        }
    }
}
