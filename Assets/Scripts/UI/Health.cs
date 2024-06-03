using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private FloatValueSO currentHealth;

    [SerializeField] private GameObject bloodParticle;

    [SerializeField] private Renderer renderer;
    [SerializeField] private float flashTime = 0.2f;

    [SerializeField]
    private bool isDead = false;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    
    private void Start()
    {
        currentHealth.value = 10;
    }

    public void AddHealth(int healthBoost)
    {
        if (currentHealth.value + healthBoost > maxHealth)
        {
            currentHealth.value = maxHealth;
        }
        else
        {
            currentHealth.value += healthBoost;
        }
        //int health = Mathf.RoundToInt(currentHealth.Value * maxHealth);
        //int val = health + healthBoost;
        //currentHealth.Value = (val > maxHealth ? maxHealth : val / maxHealth);
    }

    public void Reduce(int damage)
    {
        currentHealth.value -= damage / maxHealth;
        CreateHitFeedback();
        if (currentHealth.value <= 0)
        {
            Die();
        }
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead) // If already dead, do nothing
            return;
        if (sender.layer == gameObject.layer)
            return; // Don't take damage from objects on the same layer
        
        currentHealth.value -= amount; // Decrease health by the damage amount

        if (currentHealth.value > 0) 
        {
            OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
        }
        else
        {
            OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
            isDead = true; // Mark as dead
            Destroy(gameObject); // Destroy the GameObject
        }
    }

    private void CreateHitFeedback()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        StartCoroutine(FlashFeedback());
    }

    private IEnumerator FlashFeedback()
    {
        renderer.material.SetInt("_Flash", 1);
        yield return new WaitForSeconds(flashTime);
        renderer.material.SetInt("_Flash", 0);
    }

    private void Die()
    {
        Debug.Log("Died");
        currentHealth.value = 0;
        isDead = true;
    }  

}
