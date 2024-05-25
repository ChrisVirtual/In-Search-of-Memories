using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;

//Quest step for killing Enemies
public class SlayEnemiesQuestStep : QuestStep
{
    private int enemiesKilled = 0; //Number of enemies
    private int enemiesToKill = 5; //Number of enemies required to complete the quest step

    void Start()
    {
        UpdateState(); //Update the state of the quest step when it starts
    }

    private void OnEnable()
    {
        //Subscribe to the event for when a Enemy is slain
        GameEventsManager.instance.miscEvents.onEnemyDeath += enemyDeath;
    }

    private void OnDisable()
    {
        //Unsubscribe from the event for when a coin is collected
        GameEventsManager.instance.miscEvents.onEnemyDeath -= enemyDeath;
    }

    private void enemyDeath()
    {
        //Increment the slain enemies and update the state
        if (enemiesKilled < enemiesToKill)
        {
            enemiesKilled++;
            UpdateState();
        }

        //If enough enemies have been slain, finish the quest step
        if (enemiesKilled >= enemiesToKill)
        {
            FinishQuestStep();
        }
    }

    //Update the state of the quest step based on the number of slain enemies
    private void UpdateState()
    {
        string state = enemiesKilled.ToString();
        string status = "Slain " + enemiesKilled + " / " + enemiesToKill + " Enemies.";
        ChangeState(state, status); //Update the quest step state
    }

    //Set the quest step state when initializing based on the provided state
    protected override void SetQuestStepState(string state)
    {
        this.enemiesKilled = System.Int32.Parse(state); //Parse the state string to get the number of coins collected
        UpdateState(); //Update the state of the quest step
    }
}
