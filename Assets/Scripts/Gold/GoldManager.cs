using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    // Singleton instance
    private static GoldManager _instance;
    public static GoldManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GoldManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GoldManager");
                    _instance = obj.AddComponent<GoldManager>();
                }
            }
            return _instance;
        }
    }

    //Variable to store the current gold amount
    public int gold;

    //Reference to the TextMeshProUGUI component for displaying gold
    public TextMeshProUGUI goldDisplay;

    //Reference to the GoldEvents script for handling gold-related events
    public GoldEvents goldEvents;

    private void Start()
    {
        //Get the GoldEvents instance from the GameEventsManager
        goldEvents = GameEventsManager.instance.goldEvents;
    }

    private void OnEnable()
    {
        //Subscribe to events
        GameEventsManager.instance.goldEvents.onGoldGained += GoldGained;
        GameEventsManager.instance.goldEvents.onGoldChange += UpdateGoldUI;
    }

    private void OnDisable()
    {
        //Unsubscribe from events
        GameEventsManager.instance.goldEvents.onGoldGained -= GoldGained;
        GameEventsManager.instance.goldEvents.onGoldChange -= UpdateGoldUI;
    }

    //Method called when gold is gained
    private void GoldGained(int goldAmount)
    {
        //Add the gained gold amount to the total gold
        gold += goldAmount;

        //Invoke GoldChange event to update UI
        goldEvents.GoldChange(gold);
    }

    //Method to update the UI with the current gold amount
    private void UpdateGoldUI(int currentGold)
    {
        //Update the text displayed in the UI
        goldDisplay.text = " Gold: " + gold;
    }
}