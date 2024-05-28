using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRender, weaponRenderer;
    private Transform weaponTransform;
    private Vector3 scale;

    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;

    public Transform circleOrigin;
    public float radius;
    public LayerMask detectionLayerMask;

    public bool isAttacking { get; private set; }

    public void ResetIsAttacking()
    {
        isAttacking = false;
    }

    public void Awake()
    {
        weaponTransform = transform;
        scale = weaponTransform.localScale;
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        if (isAttacking)
            return;
        Vector3 direction = (targetPosition - weaponTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Flip the weapon vertically based on the direction
        if (direction.x < 0)
        {
            weaponTransform.localScale = new Vector3(scale.x, -scale.y, scale.z);
        }
        else
        {
            weaponTransform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    public void Attack()
    {
        if (attackBlocked)
        {
            return;
        }
        animator.SetTrigger("Attack");
        isAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 positon = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(positon, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            if (collider is CircleCollider2D)
            {
                continue;
            }
            
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
                Debug.Log("Hit enemy for 1");
            }
        }
    }
}
