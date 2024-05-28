using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraSpeed = 2f;
    public Transform target;

    void Start()
    {
        // Ensure the target is assigned when the game starts or continues
        if (target == null)
        {
            FindPlayer();
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
        }

        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, CameraSpeed * Time.deltaTime);
        }
    }

    public void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found");
        }
    }
}
