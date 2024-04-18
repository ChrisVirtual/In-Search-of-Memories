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

    private bool isMovingToSpot = true;
    private EnemyState currentState;

    protected override void Start()
    {
        base.Start();

        timeBetweenShots = startTimeBetweenShots;
        moveSpot = new GameObject().transform; // Create a new empty GameObject to serve as the move spot
    }

    protected override void Attack()
    {
        if (timeBetweenShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
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
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

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

        if (distance > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } 
        else if (distance < stoppingDistance && distance > retreatDistance)
        {
            transform.position = this.transform.position;
        } 
        else if (distance < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }

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

    public enum EnemyState
    {
        Patrol,
        TrackPlayer
    }
}
