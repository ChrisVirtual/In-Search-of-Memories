using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    // Attack cooldown settings
    public float attackCooldown = 2.0f;
    private float currentCooldown = 0.0f;

    // Current enemy state
    private EnemyState currentState;

    // Patrol wait time settings
    private float waitTime;
    public float startWaitTime;

    // Patrol movement spot settings
    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Layer masks for collision detection
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    // Attack collider reference
    [SerializeField]
    private CapsuleCollider2D attackCollider;

    // Rigidbody component for movement
    private Rigidbody2D rb;

    // Movement control variables
    private bool isMovingToSpot = true;
    private bool isPlayerInRange = false;

    private void Awake()
    {
        moveSpot = new GameObject().transform;
        attackCollider.isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Handle player entering attack range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            currentState = EnemyState.TrackPlayer;
            Debug.Log("Player entered attack range. Switching to TrackPlayer state");
        }
    }

    // Handle player exiting attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            currentState = EnemyState.Patrol;
            Debug.Log("Player exited attack range. Switching to Patrol state");
        }
    }

    // Execute attack logic
    protected override void Attack()
    {
        if (currentCooldown <= 0 && isPlayerInRange)
        {
            Debug.Log("Melee enemy attacking player");
            // Add code here to damage the player

            currentCooldown = attackCooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    // Update method to handle state-specific behaviors
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

    // Patrol behavior logic
    void PatrolBehavior()
    {
        if (isMovingToSpot)
        {
            // Calculate movement direction and next position
            Vector2 moveDirection = (moveSpot.position - transform.position).normalized;
            Vector2 nextPosition = (Vector2)transform.position + moveDirection * speed * Time.deltaTime;

            if (IsWalkable(nextPosition))
            {
                rb.velocity = moveDirection * speed;
            }
            else
            {
                ResetPatrol();
            }

            // Check if reached the move spot
            if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                ResetPatrol();
            }
        }
        else
        {
            // Handle waiting behavior
            if (waitTime <= 0)
            {
                ResetPatrol();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    // Track player behavior logic
    void TrackPlayerBehavior()
    {
        // Calculate distance and direction to the player
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;

        // Check if player is out of attack range
        if (distance > attackCollider.size.x)
        {
            Vector2 nextPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

            if (IsWalkable(nextPosition))
            {
                rb.velocity = direction * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            Attack();
        }
    }

    // Reset patrol behavior
    void ResetPatrol()
    {
        // Generate a new move spot and reset movement
        Vector2 newMoveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        moveSpot.position = newMoveSpot;
        isMovingToSpot = true;
        waitTime = startWaitTime;

        Vector2 moveDirection = (moveSpot.position - transform.position).normalized;
        rb.velocity = moveDirection * speed;
    }

    // Check if the target position is walkable
    private bool IsWalkable(Vector3 targetPos)
    {
        bool isWalkable = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) == null;

        if (!isWalkable)
        {
            Debug.Log("Collision detected at: " + targetPos);
        }

        return isWalkable;
    }

    // Enemy states
    public enum EnemyState
    {
        Patrol,
        TrackPlayer
    }
}
