using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Import the GoldEvents namespace to use its types directly
using static GoldEvents;

public class GameEventsManager : MonoBehaviour
{
    //Singleton instance
    public static GameEventsManager instance;

    //Event instances
    public GoldEvents goldEvents; //Events related to gold
    public MiscEvents miscEvents; //Miscellaneous events
    public QuestEvents questEvents; //Events related to quests
    public InputEvents inputEvents; //Events related to user input
    public PlayerEvents playerEvents; //Events related to the player

    private void Awake()
    {
        Debug.Log("GameEventsManagerAwake");
        //Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize event instances
        goldEvents = new GoldEvents();
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
    }
}
