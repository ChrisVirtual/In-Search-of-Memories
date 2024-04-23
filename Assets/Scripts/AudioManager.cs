using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // References to audio sources for music and sound effects
    [Header("-----Audio Source-------")]
    [SerializeField] AudioSource musicSource; // Reference to the audio source for music
    [SerializeField] AudioSource SFXSource; // Reference to the audio source for sound effects

    // Audio clips for background music and sound effects
    [Header("-----Audio Clip-------")]
    public AudioClip background; // Background music clip
    public AudioClip collision; // Collision sound effect clip
    public AudioClip dialog; // Collision sound effect clip
    public AudioClip NPCInteraction; // NPC interaction sound effect clip

    // Called at the start of the script's execution
    private void Start()
    {
        musicSource.clip = background; // Set background music clip
        musicSource.Play(); // Start playing background music
    }

    // Plays a sound effect once
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); // Play the specified sound effect
    }
}
