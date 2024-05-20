using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCamera; // Reference to the minimap camera
    public float zoomSpeed = 2.0f; // Speed of zooming
    public float minZoom = 2.0f; // Minimum zoom level
    public float maxZoom = 10.0f; // Maximum zoom level

    void Update()
    {
        // Check if the minimap camera is assigned
        if (minimapCamera != null)
        {
            // Get the scroll input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Adjust the orthographic size for orthographic camera
            if (minimapCamera.orthographic)
            {
                minimapCamera.orthographicSize -= scrollInput * zoomSpeed;
                minimapCamera.orthographicSize = Mathf.Clamp(minimapCamera.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                // Adjust the field of view for perspective camera
                minimapCamera.fieldOfView -= scrollInput * zoomSpeed;
                minimapCamera.fieldOfView = Mathf.Clamp(minimapCamera.fieldOfView, minZoom, maxZoom);
            }
        }
    }
}
