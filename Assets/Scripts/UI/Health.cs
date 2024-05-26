using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;

    [SerializeField]
    private FloatValueSO currentHealth;

    [SerializeField]
    private GameObject bloodParticle;

    [SerializeField]
    private Renderer renderer;

    [SerializeField]
    private float flashTime = 0.2f;

    [SerializeField]
    private bool isDead = false;

    public UnityEvent<GameObject> OnHitWithReference,
        OnDeathWithReference;

    public GameObject player;
    private EnemyHealthBar enemyHealthBar;


    private void Start()
    {
        currentHealth.Value = 10;
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through it's tag
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        if (enemyHealthBar != null)
        {
            enemyHealthBar.UpdateHealthBar(currentHealth.Value, maxHealth);
        }
    }

    public float getCurrentHealth()
    { 
        return currentHealth.Value;
    }
    public void AddHealth(int healthBoost)
    {
        if (currentHealth.Value + healthBoost > maxHealth)
        {
            currentHealth.Value = maxHealth;
        }
        else 
        {
            currentHealth.Value += healthBoost;
        }
        //int health = Mathf.RoundToInt(currentHealth.Value * maxHealth);
        //int val = health + healthBoost;
        //currentHealth.Value = (val > maxHealth ? maxHealth : val / maxHealth);
    }

    public void Reduce(int damage)
    {
        //currentHealth.Value -= damage / maxHealth;
        currentHealth.Value -= damage;
        CreateHitFeedback();
        if (currentHealth.Value <= 0)
        {
            Die();
        }
        if(enemyHealthBar != null) 
        {
            enemyHealthBar.UpdateHealthBar(currentHealth.Value, maxHealth);
        }
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead) // If already dead, do nothing
            return;
        if (sender.layer == gameObject.layer)
            return; // Don't take damage from objects on the same layer

        currentHealth.Value -= amount; // Decrease health by the damage amount

        if (currentHealth.Value > 0)
        {
            OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
        }
        else
        {
            OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
            isDead = true; // Mark as dead
            if (gameObject.tag != "Player")
            {
                GameEventsManager.instance.miscEvents.enemyDeath();
                Debug.Log("Enemy killed");
            }
            Destroy(gameObject); // Destroy the GameObject
        }

        if (enemyHealthBar != null)
        {
            enemyHealthBar.UpdateHealthBar(currentHealth.Value, maxHealth);
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
        currentHealth.Value = 0;
        isDead = true;
    }
}
