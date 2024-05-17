using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float attackEndTime = 0.625f;
    public bool isMoving;
    public bool isAttacking;
    public bool isDashing;

    private Vector2 input;
    [SerializeField]
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public GameState state;
    public Transform circleOrigin;
    public float radius;
    public float dashDistance;
    public float dashDuration;

    public void HandleUpdate()
    {
        if(DialogManagerInk.instance.dialogIsPlaying) 
        {
            return; 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E was pushed");
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }

        if (!isMoving && !isAttacking && !isDashing) // Check if player sprite is not moving
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
                Debug.Log("Attacking!"); // Print message to console
                Vector3 mousePosition = GetMouseWorldPositon();
                Vector3 attackDir = (mousePosition - transform.position).normalized;
                animator.SetTrigger("Attack");
                isAttacking = true;
                StartCoroutine(AttackRoutine());
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash());
            }
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDashing", isDashing);
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackEndTime);
        isAttacking = false;
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
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

    IEnumerator Dash()
    {
        isDashing = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(input.x, input.y, 0f) * dashDistance;

        // Checks if there's anything solid in the path of the player's dash.
        RaycastHit2D hit = Physics2D.Raycast(startPos, endPos - startPos, Vector3.Distance(startPos, endPos), solidObjectsLayer);

        if (hit.collider != null)
        {
            // If there's a collision, stop at the hit box
            endPos = hit.point;

            // Calculate the normal vector of the collision
            Vector3 normal = hit.normal;

            // Reflect the collsion of the original dash to find bounce distance
            Vector3 reflectedDirection = Vector3.Reflect(endPos - startPos, normal).normalized;

            float bounceFactor = 0.3f; // Control bounce strength
            reflectedDirection *= bounceFactor;

            // Set the new end position to continue the dash in the reflected direction
            endPos = startPos + reflectedDirection * dashDistance;
        }

        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            float dashProgress = dashTimer / dashDuration;
            transform.position = Vector3.Lerp(startPos, endPos, dashProgress);
            dashTimer += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isDashing = false;
    }
}