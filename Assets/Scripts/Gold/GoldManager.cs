using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldDisplay;
    private GoldEvents goldEvents;

    private void Start()
    {
        goldEvents = GameEventsManager.instance.goldEvents;
    }
    private void OnEnable()
    {
        // Subscribe to events
        GameEventsManager.instance.goldEvents.onGoldGained += GoldGained;
        GameEventsManager.instance.goldEvents.onGoldChange += UpdateGoldUI;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        GameEventsManager.instance.goldEvents.onGoldGained -= GoldGained;
        GameEventsManager.instance.goldEvents.onGoldChange -= UpdateGoldUI;
    }

    private void GoldGained(int goldAmount)
    {
        gold += goldAmount;
        goldEvents.GoldChange(gold);
    }

    private void UpdateGoldUI(int currentGold)
    {
        goldDisplay.text = " Gold: " + gold;
    }
}
