using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject player;
    public float timer;

    public bool moveToPlayer;
    public float speed;
    public Rigidbody2D rigidBody2D;
    public int goldAmount = 10;

    AudioManager audioManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through it's tag
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
        //If the coin is not moving towards the player
        if (!moveToPlayer)
        {
            //Increment the timer
            if (timer < 1)
            {
                timer += Time.fixedDeltaTime;
            }
            //Once the timer reaches 1, start moving towards the player
            else
            {
                moveToPlayer = true;
                rigidBody2D.gravityScale = 0; //Disable gravity to avoid affecting movement
            }
        }

        //If the coin should move towards the player
        if (moveToPlayer)
        {
            //Calculate the movement vector towards the player
            Vector3 movementVector = player.transform.position - transform.position;
            //Set the velocity of the rigidbody to move towards the player
            rigidBody2D.velocity = movementVector * speed;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If the coin touches the player
        if (collision.gameObject.tag == "Player")
        {
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.dialog); // Usage of dialog sfx for simpler understanding.
            }

            //Notify the game events manager that gold is gained
            GameEventsManager.instance.goldEvents.GoldGained(goldAmount);
            //Notify the game events manager that a coin is collected
            GameEventsManager.instance.miscEvents.coinCollected();
            //Destroy the coin
            Destroy(gameObject);
        }
    }

}