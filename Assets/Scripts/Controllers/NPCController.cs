using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;

    AudioManager audioManager; // Reference to the AudioManager script

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Get reference to AudioManager
    }

    public void Interact() // Function of interact method that when player interacts with an NPC triggers a Dialog to happen.
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
