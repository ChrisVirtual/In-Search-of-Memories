using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoldEvents;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;

    // Event instances
    public GoldEvents goldEvents = new GoldEvents();
    public MiscEvents miscEvents = new MiscEvents();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

