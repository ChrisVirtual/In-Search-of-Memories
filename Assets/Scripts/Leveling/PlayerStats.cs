using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //Configuration variables
    [Header("Configuration")]
    [SerializeField] private int startingLevel = 1;
    [SerializeField] private int startingExperience = 0;

    //Player stats
    [SerializeField] public int currentLevel;
    public int statPoints;
    public FloatValueSO currentHealth;
    public float maxHealth;
    public FloatValueSO currentMana;
    public int maxMana;
    public int currentExp; //Current exp for this level
    public int maxExp; //Max exp for this level
    public int vitality; //Increases health 
    public int strength; //Increases attack damage
    public int dexterity; //Increases attack speed
    public int intelligence; //Increases mana
    public int speed; //Increases speed
    public Health health;
    public Mana mana;
    //UI elements
    public Slider healthBar;
    public Slider manaBar;
    public Slider expBar;
    public TextMeshProUGUI statIncreaseText;
    public TextMeshProUGUI healthSliderDisplay;
    public TextMeshProUGUI manaSliderDisplay;
    public TextMeshProUGUI levelSliderDisplay;

    public static PlayerStats instance;

    public float getMovementSpeed()
    {
        return speed * 0.25f;
    }
    public void setMaxHealth()
    {
        health.maxHealth = 95 + (vitality * 5);
    }

    public void setMaxMana()
    {
        mana.maxMana = 18 + (intelligence * 2);
    }

    public float getAttackDamage()
    {
        return strength * 0.25f;
    }

    public float getAttackSpeed()
    {
        return dexterity * 0.25f;
    }

    private void Awake()
    {
        //Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        //Subscribe to experience gained event
        GameEventsManager.instance.playerEvents.onExperienceGained += ExperienceGained;
    }

    private void OnDisable()
    {
        //Unsubscribe from experience gained event
        GameEventsManager.instance.playerEvents.onExperienceGained -= ExperienceGained;
    }

    private void Start()
    {
        //Initialize UI elements
        GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
        GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExp);
    }

    private void ExperienceGained(int experience)
    {
        //Update current experience
        currentExp += experience;

        //Level up logic
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
        //Update UI sliders and texts
        changeSliderUI();
    }

    public void changeSliderUI()
    {
        //Update UI slider values
        healthBar.value = currentHealth.Value;
        manaBar.value = currentMana.Value;
        expBar.value = currentExp;

        //Update UI slider maximum values
        healthBar.maxValue = health.maxHealth;
        manaBar.maxValue = mana.maxMana;
        expBar.maxValue = maxExp;

        //Update UI text displays
        healthSliderDisplay.text = currentHealth.Value + " / " + health.maxHealth;
        manaSliderDisplay.text = currentMana.Value + " / " + mana.maxMana;
        levelSliderDisplay.text = " Level: " + currentLevel;
    }
}