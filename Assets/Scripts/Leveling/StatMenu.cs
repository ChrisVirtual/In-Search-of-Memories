using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatMenu : MonoBehaviour
{
    //References to UI TextMeshProUGUI elements
    public TextMeshProUGUI vitalityStat;
    public TextMeshProUGUI strengthStat;
    public TextMeshProUGUI dexterityStat;
    public TextMeshProUGUI intelligenceStat;
    public TextMeshProUGUI speedStat;
    public TextMeshProUGUI statPointsRemaining;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    //[SerializeField] private GameObject statIncreaseButton;
    [SerializeField] private GameObject statMenu;

    //Reference to PlayerStats scriptable object
    public PlayerStats playerStats;

    //Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleStatMenuUI();
        }
        //Check if the stat menu is active and display it
        if (statMenu.gameObject.activeInHierarchy)
        {
            displayStatMenu();
        }

        //Show visual cue if there are stat points available
        if (playerStats.statPoints > 0)
        {
            visualCue.gameObject.SetActive(true);
        }
        else
        {
            visualCue.gameObject.SetActive(false);
        }
    }

    private void ToggleStatMenuUI()
    {
        statMenu.SetActive(!statMenu.activeSelf);
    }
    //Method to increase a specific stat based on statID
    public void IncreaseStat(int statID)
    {
        //Check statID and available stat points before increasing the stat
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

        //Display the stat menu after increasing the stat
        displayStatMenu();
    }

    //Method to display stat menu UI elements
    public void displayStatMenu()
    {
        //Update the text of UI elements with player stats and remaining stat points
        vitalityStat.text = " VIT: " + playerStats.vitality;
        strengthStat.text = " STR: " + playerStats.strength;
        dexterityStat.text = " DEX: " + playerStats.dexterity;
        intelligenceStat.text = " INT: " + playerStats.intelligence;
        speedStat.text = " SPD: " + playerStats.speed;

        statPointsRemaining.text = " Stat Points Remaining: " + playerStats.statPoints;
    }
}
