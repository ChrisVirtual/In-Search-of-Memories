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

    public void Awake()
    {
        weaponTransform = transform;
        scale = weaponTransform.localScale;
    }

    public void RotateTowards(Vector3 targetPosition)
    {
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

    public void Attack() {
        if(attackBlocked) {
            return;
        }
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack() {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
