using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Adjust the speed at which the camera moves
    public float CameraSpeed = 2f;

    // Reference the player
    public Transform target;

    void LateUpdate()
    {
        // Calculate the position for the camera
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);

        // moves the camera towards the new position
        transform.position = Vector3.Slerp(transform.position, newPos, CameraSpeed * Time.deltaTime);
    }
}
