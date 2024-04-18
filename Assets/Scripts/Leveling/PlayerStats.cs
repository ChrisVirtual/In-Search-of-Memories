using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int startingLevel = 1;
    [SerializeField] private int startingExperience = 0;

    public int currentLevel; 
    public int statPoints;
    public int currentHealth; 
    public int maxHealth;

    public int currentMana;
    public int maxMana;

    public int currentExp; //Current exp for this level
    public int maxExp; //Max exp for this level

    public int vitality; //Increases health
    public int strength; //Increases attack damage
    public int dexterity; //Increases attack speed
    public int intelligence; //Increases 
    public int speed; //Increases speed and roll distance
    
    public Slider healthBar;
    public Slider manaBar;
    public Slider expBar;

    public TextMeshProUGUI statIncreaseText;
    public TextMeshProUGUI healthSliderDisplay;
    public TextMeshProUGUI manaSliderDisplay;
    public TextMeshProUGUI levelSliderDisplay;

    private void Awake()
    {
        currentLevel = startingLevel;
        currentExp = startingExperience;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onExperienceGained += ExperienceGained;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onExperienceGained -= ExperienceGained;
    }

    private void Start()
    {
        GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
        GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExp);
    }

    private void ExperienceGained(int experience)
    {
        currentExp += experience;
        // check if we're ready to level up
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
        changeSliderUI();

    }

    public void changeSliderUI()
    { 
        healthBar.value = currentHealth;
        manaBar.value = currentMana;
        expBar.value = currentExp;

        healthBar.maxValue = maxHealth;
        manaBar.maxValue = maxMana;
        expBar.maxValue = maxExp;

        healthSliderDisplay.text = currentHealth + " / " + maxHealth;
        manaSliderDisplay.text = currentMana + " / " + maxMana;
        levelSliderDisplay.text = " Level: " + currentLevel;
    }
}
