using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : Detector
{
    [SerializeField]
    private float detectionRange = 5f;

    private Transform playerTransform;
    private bool detected = false;
    private Vector2 detectedPosition;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Detect()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            detected = true;
            detectedPosition = playerTransform.position;
        }
        else
        {
            detected = false;
        }
    }

    public override bool HasDetected()
    {
        return detected;
    }

    public override Vector2 GetDetectedPosition()
    {
        return detectedPosition;
    }
}
