using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f;

    [SerializeField]
    private float chaseSpeed = 2f;   // Speed when chasing the player

    [SerializeField]
    private float patrolSpeed = 1f;  // Speed when patrolling or moving without chasing the player

    // Inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField]
    private Vector2 movementInput;

    bool following = false;

    [SerializeField]
    private EnemyMovement enemyMovement;

    private void Start()
    {
        // Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);

        // Subscribe the Move method to the OnMovementInput event
        OnMovementInput.AddListener(enemyMovement.Move);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect();
        }
    }

    private void Update()
    {
        // Check if the enemy has detected the player
        if (HasDetectedPlayer())
        {
            if (!following)
            {
                following = true;
                StartCoroutine(Chase());
            }
        }
        else
        {
            // Stop chasing if player is not detected
            following = false;
        }
    }

    private bool HasDetectedPlayer()
    {
        return detectors.Any(detector => detector.HasDetected());
    }

    private IEnumerator Chase()
    {
        const float playerBufferDistance = 0.5f; // Adjust this value as needed

        Debug.Log("Chase Coroutine Started");
        while (HasDetectedPlayer())
        {
            Debug.Log("Player Detected. Chasing...");

            // Calculate the direction towards the player
            Vector2 direction = (detectors[0].GetDetectedPosition() - (Vector2)transform.position).normalized;

            // Update movement input
            movementInput = direction * chaseSpeed;
            Debug.Log("Movement Input: " + movementInput);

            // Update pointer input to look at the player
            OnPointerInput?.Invoke(detectors[0].GetDetectedPosition());

            // Check if the enemy is close enough to the player
            if (Vector2.Distance(transform.position, detectors[0].GetDetectedPosition()) < playerBufferDistance)
            {
                // Stop chasing and wait before checking again
                movementInput = Vector2.zero;
                yield return new WaitForSeconds(aiUpdateDelay);
            }
            else
            {
                // Invoke the movement input event
                OnMovementInput.Invoke(movementInput);
            }

            yield return new WaitForSeconds(aiUpdateDelay);
        }

        // Stopping Logic
        Debug.Log("Stopped Chasing");
        movementInput = Vector2.zero;
        following = false;
    }
}
