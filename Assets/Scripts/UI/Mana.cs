using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Mana : MonoBehaviour
{
    [SerializeField] public float maxMana;
    [SerializeField] private FloatValueSO currentMana;
    public PlayerStats playerStats;
    float regenTime;
    private void Start()
    {
        StartCoroutine(ManaRegen());
        currentMana.Value = 10;
    }
  
    public void ConsumeMana(int manaConsumption)
    {
        if (currentMana.Value >= manaConsumption)
        {
            currentMana.Value -= manaConsumption;
        }
        else 
        {
            Debug.Log("You are out of Mana");
        }
        
    }

    public void AddMana(int manaBoost)
    {
        if (currentMana.Value + manaBoost > maxMana)
        {
            currentMana.Value = maxMana;
        }
        else
        { 
            currentMana.Value += manaBoost;
        }
    }

    private IEnumerator ManaRegen()
    { 
        while (true) 
        {
            regenTime = 5f / playerStats.intelligence;
            yield return new WaitForSeconds(regenTime);
            if(currentMana.Value < maxMana)
            {
                currentMana.Value += 1f;
                if (currentMana.Value > maxMana)
                {
                    currentMana.Value = maxMana;
                }
            }
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) 
        {
            ConsumeMana(5);
            Debug.Log("Spell test");
        }
    }
}
