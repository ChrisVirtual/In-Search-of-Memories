using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    
    public int level; 
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
   



    public void Update()
    {
        changeSliderUI();
        if (currentExp >= maxExp)
        {
            int leftOverExp = 0;
            if (currentExp > maxExp)
            {
                leftOverExp = currentExp - maxExp;
            }

            maxExp = (int)(maxExp * 1.5f);
            level++;
            statPoints += 5;
            currentExp = leftOverExp;
        }
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
        levelSliderDisplay.text = " Level: " + level;
        
    }

    //public void updateStatMenu()
    //{
    //    vitalityStat.text = " VIT: " + vitality;
    //    strengthStat.text = " STR: " + strength;
    //    dexterityStat.text = " DEX: " + dexterity;
    //    intelligenceStat.text = " INT: " + intelligence;
    //    speedStat.text = " SPD: " + speed;

    //    statPointsRemaining.text = " Stat Points Remaining: " + statPoints;


    //}
}
