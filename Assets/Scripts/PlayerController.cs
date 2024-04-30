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
    private bool isDashing;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public GameState state;
    public Transform circleOrigin;
    public float radius;
    public float dashDistance;
    public float dashDuration;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving && !isAttacking && !isDashing)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                Vector3 targetPos = transform.position;
                targetPos.x += input.x * moveSpeed * Time.deltaTime;
                targetPos.y += input.y * moveSpeed * Time.deltaTime;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
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

    private bool IsWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) == null;
    }

    public static Vector3 GetMouseWorldPositon()
    {
        Vector3 vec3 = GetMouseWorldPositonWithZ(Input.mousePosition, Camera.main);
        vec3.z = 0f;
        return vec3;
    }

    public static Vector3 GetMouseWorldPositonWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositonWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositonWithZ(Vector3 screenPos, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Health health; 
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.gameObject);
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