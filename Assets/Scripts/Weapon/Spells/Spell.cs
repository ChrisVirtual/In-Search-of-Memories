using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;

    // Initialize the spell with a target position
    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        Destroy(gameObject, 5f); // Destroy the spell after 5 seconds 
    }

    void Update()
    {
        // Move the spell towards the target position
        if (targetPosition != null)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}