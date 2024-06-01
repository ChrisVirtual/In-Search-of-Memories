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

    // List of patrol waypoints
    public List<Vector2> waypoints;
    private int currentWaypointIndex = 0;

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

    // Reference to the enemy weapon script
    public EnemyWeaponParent enemyWeapon;

    // Reference to the Animator component for the attack animation
    public Animator enemyAnimator;

    // Attack animation trigger name
    private readonly string attackTrigger = "Attack";

    // Reference to the player's Health component
    private Health playerHealth;

    private void Awake()
    {
        attackCollider.isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        base.Start();
        enemyWeapon = GetComponentInChildren<EnemyWeaponParent>();

        if (enemyWeapon == null)
        {
            Debug.LogError("EnemyWeaponParent script not found!");
        }

        // Ensure there are waypoints assigned
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to MeleeEnemy.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            currentState = EnemyState.TrackPlayer;
            playerHealth = other.GetComponent<Health>();
            Debug.Log("Player entered attack range. Switching to TrackPlayer state");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            currentState = EnemyState.Patrol;
            playerHealth = null;
            Debug.Log("Player exited attack range. Switching to Patrol state");
        }
    }

    protected override void Attack()
    {
        if (currentCooldown <= 0 && isPlayerInRange)
        {
            Debug.Log("Melee enemy attacking player");

            if (playerHealth != null)
            {
                playerHealth.GetHit(1, gameObject);
            }

            enemyAnimator.SetTrigger(attackTrigger);
            currentCooldown = attackCooldown;
        }
        else
        {
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
            Vector2 moveDirection = (waypoints[currentWaypointIndex] - (Vector2)transform.position).normalized;
            Vector2 nextPosition = (Vector2)transform.position + moveDirection * speed * Time.deltaTime;

            if (IsWalkable(nextPosition))
            {
                rb.velocity = moveDirection * speed;
            }
            else
            {
                ResetPatrol();
            }

            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.2f)
            {
                ResetPatrol();
            }
        }
        else
        {
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

    void TrackPlayerBehavior()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;

        enemyWeapon.RotateTowards(target.position);

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

            enemyWeapon.MoveTo(target.position);
        }
        else
        {
            rb.velocity = Vector2.zero;
            Attack();
        }
    }

    void ResetPatrol()
    {
        // Move to the next waypoint in the list
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        isMovingToSpot = true;
        waitTime = startWaitTime;

        Vector2 moveDirection = (waypoints[currentWaypointIndex] - (Vector2)transform.position).normalized;
        rb.velocity = moveDirection * speed;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        bool isWalkable = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) == null;

        if (!isWalkable)
        {
            Debug.Log("Collision detected at: " + targetPos);
        }

        return isWalkable;
    }

    public enum EnemyState
    {
        Patrol,
        TrackPlayer
    }
}