using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    public void Move(Vector2 direction)
    {
        // Calculate movement amount
        Vector2 movement = direction * moveSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(movement);
    }
}

