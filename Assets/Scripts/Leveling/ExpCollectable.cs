using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectable : MonoBehaviour
{
    public GameObject player;
    public float timer;

    public bool moveToPlayer;
    public float speed;
    public Rigidbody2D rigidBody2D;
    public int expAmount = 100;
    PlayerStats playerStats;

    AudioManager audioManager;

    private void Start()
    {
        //Get reference to the player object
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through its tag
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();

        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
        }
    }

    private void FixedUpdate()
    {
        //Move towards the player
        if (!moveToPlayer)
        {
            if (timer < 1)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                moveToPlayer = true;
                rigidBody2D.gravityScale = 0;
            }
        }

        if (moveToPlayer)
        {
            //Move the orb towards the player
            Vector3 movementVector = player.transform.position - transform.position;
            rigidBody2D.velocity = movementVector * speed;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //When the exp orb touches the player
        if (collision.gameObject.tag == "Player")
        {
            //Add experience to the player's stats
            GameEventsManager.instance.playerEvents.ExperienceGained(expAmount);

            // Play the experience gained sound
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.dialog); // Assuming 'dialog' is the experience gained sound
            }

            Destroy(gameObject); // Destroy the orb
        }
    }
}
