using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float destroyDelay = 2f;
    private float elapsedTime = 0f;
    public int damage = 1; // Damage value for the projectile

    public EnemySO enemyData;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Disable gravity
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Set collision detection mode

        int enemyLayer = LayerMask.NameToLayer("EnemyColliders");
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= destroyDelay)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CapsuleCollider2D capsuleCollider = other as CapsuleCollider2D;
        if (capsuleCollider != null)
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.GetHit(enemyData.Damage, gameObject);
                Debug.Log("Projectile hit the target!");
            }
            Destroy(gameObject);
        }
    }
}

