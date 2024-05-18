using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponParent : MonoBehaviour
{
    private Transform weaponTransform;
    private Vector3 initialLocalPosition;
    private Vector3 scale;

    public void Awake()
    {
        weaponTransform = transform;
        initialLocalPosition = weaponTransform.localPosition;
        scale = weaponTransform.localScale;
    }

    // Rotate the weapon towards a target position
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

    // Move the weapon to a target position
    public void MoveTo(Vector3 targetPosition)
    {
        weaponTransform.localPosition = initialLocalPosition; // Reset local position
        weaponTransform.position = Vector3.MoveTowards(
            weaponTransform.position,
            targetPosition,
            Time.deltaTime
        );
    }
}
