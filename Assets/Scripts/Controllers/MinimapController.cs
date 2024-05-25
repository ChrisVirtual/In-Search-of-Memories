using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCamera; // Reference to the minimap camera
    public float zoomSpeed = 2.0f; // Speed of zooming
    public float minZoom = 2.0f; // Minimum zoom level
    public float maxZoom = 10.0f; // Maximum zoom level
    [SerializeField] private GameObject Map; //Object panel for the Map Expanded content
    [SerializeField] private GameObject MapButton;
    public static MinimapController instance { get; private set; } //Singleton instance of the quest log UI
    public bool mapOpen { get; private set; } = false; //Flag indicating whether the map panel is open

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //Set the singleton instance
        }

        if (MapButton != null)
        {
            Button mapButtonComponent = MapButton.GetComponent<Button>();
            if (mapButtonComponent != null)
            {
                mapButtonComponent.onClick.AddListener(ToggleMap);
            }
            else
            {
                Debug.LogError("MapButton does not have a Button component.");
            }
        }
        else
        {
            Debug.LogError("MapButton is not assigned.");
        }

    } 

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
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    private void ToggleMap()
    { 
        Map.SetActive(!Map.activeSelf);
    }

    private void OnEnable()
    {
        //Subscribe to events
        GameEventsManager.instance.inputEvents.onMapTogglePressed += MapTogglePressed;
        
    }

    private void OnDisable()
    {
        //Unsubscribe from events
        GameEventsManager.instance.inputEvents.onMapTogglePressed -= MapTogglePressed;
       
    }

    private void MapTogglePressed()
    {
        //Toggle the visibility of the quest log UI
        if (Map.activeInHierarchy)
        {
            HideUI();
            Debug.Log("HideUI");
        }
        else
        {
            ShowUI();
            Debug.Log("ShowUI");
        }
    }

    private void HideUI()
    {
        Map.SetActive(false); //Deactivate the map panel
        EventSystem.current.SetSelectedGameObject(null); //Deselect any selected game object
        mapOpen = false; //Set the flag indicating the map panel is closed
        Debug.Log("Map closed");
    }

    private void ShowUI()
    {
        Map.SetActive(true); //Activate the map panel
        mapOpen = true; //Set the flag indicating the map panel is open
        Debug.Log("Map opened");
    }
}
