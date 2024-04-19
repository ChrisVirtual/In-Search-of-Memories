using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TalkToNPC : QuestStep
{
    bool inRange = false;

    protected override void SetQuestStepState(string state)
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    public void Update()
    {
        input();
    }
    private void input()
    {
        if (inRange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                FinishQuestStep();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        inRange = false;

    }
}
