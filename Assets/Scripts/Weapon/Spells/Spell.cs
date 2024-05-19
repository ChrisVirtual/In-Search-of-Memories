using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;
    private Animation spellAnimation; // Reference to the Animation component

    void Awake()
    {
        spellAnimation = GetComponent<Animation>(); // Get the Animation component
    }

    // Initialize the spell with a target position
    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        Destroy(gameObject, 5f); // Destroy the spell after 5 seconds to avoid memory leaks

        // Play the animation (e.g., casting animation)
        if (spellAnimation != null)
        {
            spellAnimation.Play("CastingAnimation"); // Replace "CastingAnimation" with your animation clip name
        }
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