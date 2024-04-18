using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackCooldown = 2.0f;
    private float currentCooldown = 0.0f;
    private EnemyState currentState;

    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Collider for attack range and patrol radius
    private CircleCollider2D attackCollider;
    private Rigidbody2D rb;
    private bool isMovingToSpot = true;

    private void Awake()
    {
        moveSpot = new GameObject().transform; // Create a new empty GameObject to serve as the move spot
        // Initialize components
        attackCollider = gameObject.AddComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
        attackCollider.radius = 1.0f;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.TrackPlayer;
            Debug.Log("Player entered attack range. Switching to TrackPlayer state");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.Patrol;
            Debug.Log("Player exited attack range. Switching to Patrol state");
        }
    }

    protected override void Attack()
    {
        if (currentCooldown <= 0)
        {
            Debug.Log("Melee enemy attacking player");
            // Add code here to damage the player

            // Reset the cooldown
            currentCooldown = attackCooldown;
        }
        else
        {
            // Reduce the cooldown timer
            currentCooldown -= Time.deltaTime;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                PatrolBehavior();
                break;
            case EnemyState.TrackPlayer:
                TrackPlayerBehavior();
                break;
        }
    }

    void PatrolBehavior()
    {
        if (isMovingToSpot)
        {
            Vector2 moveDirection = (moveSpot.position - transform.position).normalized;
            rb.velocity = moveDirection * speed;

            if(Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                isMovingToSpot = false;
                waitTime = startWaitTime;
            }
        }
        else
        {
            if(waitTime <= 0) 
            {
                Vector2 newMoveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                moveSpot.position = newMoveSpot;
                isMovingToSpot = true;
            }
            else 
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void TrackPlayerBehavior()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackCollider.radius)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            Attack();
        }
    }

    public enum EnemyState
    {
        Patrol,
        TrackPlayer
    }
}
