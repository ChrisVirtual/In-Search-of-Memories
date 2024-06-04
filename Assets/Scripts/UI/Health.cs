using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

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

    [SerializeField]
    private EnemySO enemyData;

    public UnityEvent<GameObject> OnHitWithReference,
        OnDeathWithReference;

    public GameObject player;
    private EnemyHealthBar enemyHealthBar;

    public float enemyHealth;
    private float regenTime;

    PlayerStats playerStats;

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            currentHealth.Value = maxHealth;
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            enemyHealth = enemyData.Health;
            maxHealth = enemyData.Health;
        }
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through it's tag
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        if (enemyHealthBar != null)
        {
            enemyHealthBar.UpdateHealthBar(enemyHealth, maxHealth);
        }
    }

    public float getCurrentHealth()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            return enemyHealth;
        }
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
    }

    public IEnumerator HealthRegen(int healthBoost)
    {
        float duration = 5f; // Duration over which health is increased
        float elapsed = 0f;
        float totalElapsed = 0f;
        float timer = 10f;

        while (totalElapsed < timer)
        {
            if (elapsed < duration)
            {
                regenTime = 1f;
                yield return new WaitForSeconds(regenTime);
                if (currentHealth.Value < maxHealth)
                {
                    currentHealth.Value += (int)(healthBoost * regenTime);
                    if (currentHealth.Value > maxHealth)
                    {
                        currentHealth.Value = maxHealth;
                    }
                    elapsed += regenTime;
                }
                totalElapsed += regenTime;
            }
            else
            {
                break;
            }
        }
    }

    public void AddHealthOvertime(int healthBoost)
    {
        StartCoroutine(HealthRegen(healthBoost));
    }

    public void Reduce(int damage)
    {
        if (gameObject.CompareTag("Player"))
        {
            currentHealth.Value -= damage;
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            enemyHealth -= damage;
        }

        CreateHitFeedback();

        if (currentHealth.Value <= 0)
        {
            Die();
        }
        if (enemyHealthBar != null)
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

        if (gameObject.CompareTag("Player"))
        {
            currentHealth.Value -= amount; // Decrease health by the damage amount
            if (currentHealth.Value > 0)
            {
                OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
            }
            else
            {
                OnHitWithReference?.Invoke(sender); //Trigger the OnHitWithReference event
                Die(); // Handle player's death
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            enemyHealth -= amount;
            if (enemyHealth > 0)
            {
                OnHitWithReference?.Invoke(sender);
                enemyHealthBar.UpdateHealthBar(enemyHealth, maxHealth);
                GameEventsManager.instance.miscEvents.enemyDeath();
            }
            else
            {
                OnHitWithReference?.Invoke(sender);
                DropLoot();
                Die();
            }
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
        UnityEngine.Debug.Log("Died");
        if (gameObject.CompareTag("Player"))
        {
            currentHealth.Value = 0;
            //Open retry menu instead of destroy
            UnityEngine.Debug.Log("Player Died like a noob");
            //Destroy(gameObject);
        }
        isDead = true;
        if (gameObject.CompareTag("Enemy"))
        {
            GameEventsManager.instance.miscEvents.enemyDeath();
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }

    public void DropLoot()
    {
        UnityEngine.Debug.Log("DropLoot Called");
        GameObject ExpOrb = Resources.Load<GameObject>("ExpOrb");
        GameObject GoldCoin = Resources.Load<GameObject>("GoldCoin");

        if (ExpOrb != null)
        {
            var expCollectable = Instantiate(ExpOrb, transform.position, Quaternion.identity);
            expCollectable.GetComponent<ExpCollectable>().expAmount = enemyData.Exp;
            UnityEngine.Debug.Log("expOrb instantiated");
        }
        else
        {
            UnityEngine.Debug.Log("exp orb is null");
        }

        if (GoldCoin != null)
        {
            var goldCollectable = Instantiate(GoldCoin, transform.position, Quaternion.identity);
            goldCollectable.GetComponent<Coin>().goldAmount = enemyData.Gold;
            UnityEngine.Debug.Log("GoldCoin instantiated");

        }
        else
        {
            UnityEngine.Debug.Log("gold orb is null");
        }
    }

}
