using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI vitalityStat;
    public TextMeshProUGUI strengthStat;
    public TextMeshProUGUI dexterityStat;
    public TextMeshProUGUI intelligenceStat;
    public TextMeshProUGUI speedStat;
    public TextMeshProUGUI statPointsRemaining;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private GameObject statIncreaseButton;
    [SerializeField] private GameObject statMenu;

    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(statMenu.gameObject.activeInHierarchy)
        {
            displayStatMenu();
        }

        if (playerStats.statPoints > 0)
        {
            visualCue.gameObject.SetActive(true);
        }
        else
        {
            visualCue.gameObject.SetActive(false);
        }
    }


    public void IncreaseStat(int statID)
    {
        
        if (statID == 1 && playerStats.statPoints > 0)
        {
            playerStats.vitality++;
            playerStats.statPoints--;
        }
        if (statID == 2 && playerStats.statPoints > 0)
        {
            playerStats.strength++;
            playerStats.statPoints--;
        }
        if (statID == 3 && playerStats.statPoints > 0)
        {
            playerStats.dexterity++;
            playerStats.statPoints--;
        }
        if (statID == 4 && playerStats.statPoints > 0)
        {
            playerStats.intelligence++;
            playerStats.statPoints--;
        }
        if (statID == 5 && playerStats.statPoints > 0)
        {
            playerStats.speed++;
            playerStats.statPoints--;
        }
        
        displayStatMenu();
    }

        public void displayStatMenu()
    {
        vitalityStat.text = " VIT: " + playerStats.vitality;
        strengthStat.text = " STR: " + playerStats.strength;
        dexterityStat.text = " DEX: " + playerStats.dexterity;
        intelligenceStat.text = " INT: " + playerStats.intelligence;
        speedStat.text = " SPD: " + playerStats.speed;

        statPointsRemaining.text = " Stat Points Remaining: " + playerStats.statPoints;
    }

}
