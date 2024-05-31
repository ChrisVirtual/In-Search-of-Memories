using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public GameObject projectile;

    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Layer masks for collision detection
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    // Rigidbody component for movement
    private Rigidbody2D rb;

    private bool isMovingToSpot = true;
    private EnemyState currentState;

    // For projectile
    public GameObject projectilePrefab;
    public float projectileSpeed;

    public EnemyWeaponParent enemyWeapon;

    public Animator enemyAnimator;

    [SerializeField]
    private string attackTrigger = "Cast";

    protected override void Start()
    {
        base.Start();

        timeBetweenShots = startTimeBetweenShots;
        moveSpot = new GameObject().transform;
        rb = GetComponent<Rigidbody2D>();

        enemyWeapon = GetComponentInChildren<EnemyWeaponParent>();

        if (enemyWeapon == null)
        {
            Debug.LogError("EnemyWeaponParent script not found!");
        }
    }

    protected override void Attack()
    {
        if (timeBetweenShots <= 0)
        {
            // Spawn the projectile
            GameObject projectileObject = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb = projectileObject.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = projectileObject.AddComponent<Rigidbody2D>();
            }

            // Set the velocity of the projectile
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;

            timeBetweenShots = startTimeBetweenShots;
            enemyAnimator.SetTrigger("Cast");
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
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
            Vector2 nextPosition = Vector2.MoveTowards(
                transform.position,
                moveSpot.position,
                speed * Time.deltaTime
            );

            if (IsWalkable(nextPosition))
            {
                transform.position = nextPosition;
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

    void TrackPlayerBehavior()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;

        // Rotate the weapon towards the player
        enemyWeapon.RotateTowards(target.position);

        if (distance > stoppingDistance)
        {
            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );
        }
        else if (distance < stoppingDistance && distance > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (distance < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                -speed * Time.deltaTime
            );
        }

        // Attack if within range
        Attack();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.TrackPlayer;
            Debug.Log("Player entered attack range. Switching to TrackPlayer state");
        }
        else
        {
            Debug.Log("Detected object with tag: " + other.tag);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.Patrol;
            Debug.Log("Player exited attack range. Switching to Patrol state");
        }
        else
        {
            Debug.Log("Exited detection of object with tag: " + other.tag);
        }
    }

    void ResetPatrol()
    {
        // Generate a new move spot and reset movement
        Vector2 newMoveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        moveSpot.position = newMoveSpot;
        isMovingToSpot = true;
        waitTime = startWaitTime;

        transform.position = Vector2.MoveTowards(
            transform.position,
            moveSpot.position,
            speed * Time.deltaTime
        );
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        bool isWalkable =
            Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) == null;

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
