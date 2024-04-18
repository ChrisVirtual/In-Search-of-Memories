using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GoldEvents;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;

    // Event instances
    public GoldEvents goldEvents;
    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    private void Awake()
    {
        Debug.Log("GameEventsManagerAwake");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
         
        }
        else
        {
            Destroy(gameObject);
        }

        goldEvents = new GoldEvents();
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
    }
}

