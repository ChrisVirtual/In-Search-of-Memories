using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float interactionRadius = 1f;

    private void Awake()
    {
        visualCue.SetActive(false);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionRadius)
        {
           
            // You can trigger the dialogue here using inkJSON
            if (!DialogManagerInk.instance.dialogIsPlaying)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DialogManagerInk.instance.EnterDialogMode(inkJSON);
                }
            }
        }
       
    }

    // This method can be used to set the player's transform when it enters the trigger zone
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    // This method can be used to set the interaction radius
    public void SetInteractionRadius(float radius)
    {
        interactionRadius = radius;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            visualCue.SetActive(true);
           
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            visualCue.SetActive(false);
        }
    }
}
