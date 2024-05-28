using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-------")]
    [SerializeField] AudioSource musicSource; // Reference to the audio source for music
    [SerializeField] AudioSource SFXSource; // Reference to the audio source for sound effects

    [Header("-----Audio Clip-------")]
    public AudioClip background; // Background music clip
    public AudioClip collision; // Collision sound effect clip
    public AudioClip dialog; // Dialog sound effect clip
    public AudioClip NPCInteraction; // NPC interaction sound effect clip

    private void Start()
    {
        if (musicSource != null)
        {
            musicSource.clip = background; // Set background music clip
            musicSource.Play(); // Start playing background music
        }
        else
        {
            Debug.LogWarning("Music source is not assigned.");
        }
    }

    // Plays a sound effect once
    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null && clip != null)
        {
            SFXSource.PlayOneShot(clip); // Play the specified sound effect
        }
        else
        {
            if (SFXSource == null)
            {
                Debug.LogWarning("SFX source is not assigned.");
            }

            if (clip == null)
            {
                Debug.LogWarning("Attempted to play a null audio clip.");
            }
        }
    }
}
