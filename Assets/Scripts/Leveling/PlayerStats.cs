using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    // Configuration variables
    [Header("Configuration")]
    [SerializeField] private int startingLevel = 1;
    [SerializeField] private int startingExperience = 0;

    // Player stats
    [SerializeField] public int currentLevel;
    public int statPoints;
    public FloatValueSO currentHealth;
    public float maxHealth;
    public int currentMana;
    public int maxMana;
    public int currentExp;
    public int maxExp;
    public int vitality;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int speed;

    // UI elements
    public Slider healthBar;
    public Slider manaBar;
    public Slider expBar;
    public TextMeshProUGUI statIncreaseText;
    public TextMeshProUGUI healthSliderDisplay;
    public TextMeshProUGUI manaSliderDisplay;
    public TextMeshProUGUI levelSliderDisplay;

    public static PlayerStats instance;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            currentLevel = startingLevel;
            currentExp = startingExperience;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to experience gained event
        GameEventsManager.instance.playerEvents.onExperienceGained += ExperienceGained;
    }

    private void OnDisable()
    {
        // Unsubscribe from experience gained event
        GameEventsManager.instance.playerEvents.onExperienceGained -= ExperienceGained;
    }

    private void Start()
    {
        // Initialize UI elements
        GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
        GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExp);
    }

    private void ExperienceGained(int experience)
    {
        // Update current experience
        currentExp += experience;

        // Level up logic
        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            maxExp = (int)(maxExp * 1.5f);
            currentLevel++;
            GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
            statPoints += 5;
        }
        GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExp);
    }

    public void Update()
    {
        // Update UI sliders and texts
        ChangeSliderUI();
    }

    public void ChangeSliderUI()
    {
        if (healthBar == null || manaBar == null || expBar == null) return;

        // Update UI slider values
        healthBar.value = currentHealth.value;
        manaBar.value = currentMana;
        expBar.value = currentExp;

        // Update UI slider maximum values
        healthBar.maxValue = maxHealth;
        manaBar.maxValue = maxMana;
        expBar.maxValue = maxExp;

        // Update UI text displays
        healthSliderDisplay.text = currentHealth.value + " / " + maxHealth;
        manaSliderDisplay.text = currentMana + " / " + maxMana;
        levelSliderDisplay.text = " Level: " + currentLevel;
    }

    public void LoadData(GameData data)
    {
        // Load health, mana, exp, and level data
        currentHealth.value = data.playerHealth;
        currentMana = data.currentMana;
        currentExp = data.currentExp;
        currentLevel = data.playerLevel;
        statPoints = data.statPoints;

        // Update UI after loading data
        ChangeSliderUI();
    }

    public void SaveData(ref GameData data)
    {
        if (this == null) return; // Add null check

        data.playerPosition = this.transform.position;

        // Save health and level data
        data.playerHealth = currentHealth.value;
        data.playerLevel = currentLevel;
        data.statPoints = statPoints;
    }
}
