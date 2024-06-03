using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer weaponRenderer;
    private Transform weaponTransform;
    private Vector3 scale;

    public EquippableItemSO equippableItemSO;

    public Animator animator;
    public float delay = 1f;
    private bool attackBlocked;

    public Transform circleOrigin;
    public float radius;
    public LayerMask detectionLayerMask;

    public PlayerStats playerStats;
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
        if (!gameObject.activeInHierarchy || attackBlocked)
        {
            return;
        }
        animator.SetTrigger("Attack");
        isAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    public float getAttackSpeed()
    {
        float attackSpeed = Mathf.RoundToInt(delay / (1 + playerStats.getAttackSpeed()));
        return attackSpeed;
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(getAttackSpeed());
        attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void SetEquippableItemSO(EquippableItemSO itemSO)
    {
        equippableItemSO = itemSO;
    }

    public void DetectColliders()
    {
        int damage = Mathf.RoundToInt((int)playerStats.getAttackDamage() + equippableItemSO.damage);
        if (equippableItemSO == null)
        {
            Debug.LogError("EquippableItemSO is not set in WeaponParent.");
            return;
        }

        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            if (collider is CircleCollider2D)
            {
                continue;
            }
            if (collider.CompareTag("Player"))
            {
                continue;
            }

            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(damage, transform.parent.gameObject);
            }
            else
            {
                Debug.LogWarning($"{collider.gameObject.name} does not have a Health component.");
            }
        }
    }
}
