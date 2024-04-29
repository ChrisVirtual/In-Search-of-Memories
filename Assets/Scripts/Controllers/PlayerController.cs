using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // Controls the speed that the player will move through the grid(map)
    private float attackEndTime = 0.625f; // Define how long will last the attack animation
    public bool isMoving; // Check if player is moving in the grid(map)
    public bool isAttacking;
    private Vector2 input; // Holds 2 values X and Y in the grid(map)

    [SerializeField] private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    public GameState state;

    public Transform circleOrigin;
    public float radius;

    public void HandleUpdate()
    {
        if (!isMoving && !isAttacking) // Check if player sprite is not moving
        {
            input.x = Input.GetAxisRaw("Horizontal"); // Watch if user is pressing left or right key then store in the input variable.
            input.y = Input.GetAxisRaw("Vertical"); // Watch if user is pressing up or down key, then store in the input variable.

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero) // If user is pressing a key and the value is bigger than zero it will release a function.
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                Vector3 targetPos = transform.position;
                targetPos.x += input.x * moveSpeed * Time.deltaTime; // add to the variable in x axis
                targetPos.y += input.y * moveSpeed * Time.deltaTime; // add to the variable in y axis

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }    

            if (Input.GetMouseButtonDown(0)) 
            {
                Vector3 mousePosition = GetMouseWorldPositon();
                Vector3 attackDir = (mousePosition - transform.position).normalized;
                //Debug.Log("Attack");
                animator.SetTrigger("Attack");
                isAttacking = true;
                StartCoroutine(AttackRoutine());
            } 
        }

        IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(attackEndTime);
            isAttacking = false;
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);

        if (Input.GetKeyDown(KeyCode.E)) // Button to trigger interaction from player with objects/entities
        {
            Interact();
        }
        
    }

    public void OnAttackEnd() // will be called by the animation Event when the animation be concluded
    {
        isAttacking = false;
    }
    
    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")); // makes a invisible hitline which moves according to the direction that the player character is facing
        var interactPos = transform.position + facingDir;

        // Debug.DrawLine(transform.position, interactPos, Color.red, 1f); //draw a represtation of this invisible hitline 

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer); // makes the line overlap the collider allowing interaction
        if(collider != null) // if collider is not null the coliders will check for intercatablelayer and get the component, then interact (do some function)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); //Get the original position and move towards the target position
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
        isAttacking = false;
    }

    // Determine if a position is suitable for movement
    private bool IsWalkable(Vector3 targetPos)
    {
        // Use a small overlap circle to check for collisions with solid or interactable objects
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) == null;
    }

    // --- Mouse Position Handling ---

    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPositon()
    {
        Vector3 vec3 = GetMouseWorldPositonWithZ(Input.mousePosition, Camera.main);
        vec3.z = 0f;
        return vec3;
    }
    // Get mouse position in world space with a specific camera
    public static Vector3 GetMouseWorldPositonWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositonWithZ(Input.mousePosition, worldCamera);
    }
    // Get mouse position in world space, providing both screen position and camera
    public static Vector3 GetMouseWorldPositonWithZ(Vector3 screenPos, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    // ---- Gizmos Visualization ----
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position; // Determine the circle's center (using origin object if set, otherwise use zero)
        Gizmos.DrawWireSphere(position, radius); // Draw a blue wire sphere to represent the radius in the scene editor 
    }

    // ---- Collider Detection ----
    public void DetectColliders()
    {
        // Get all colliders within the circle's radius
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            //Debug.Log(collider.name);
            Health health; 
            // Attempt to get the Health component of the collided object
            if (health = collider.GetComponent<Health>()) // If the object has a Health component, inflict damage
            {
                health.GetHit(1, transform.gameObject); // Deal 1 damage, passing this object as the reference
            }
        }
    }
}