using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectable : MonoBehaviour
{
    private GameObject player;
    private float timer = 3f;

    private bool moveToPlayer = false;
    private float speed = 3f;
    public Rigidbody2D rigidBody2D;
    public int expAmount = 100;


    private void Start()
    {
        //Get reference to the player object
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through its tag
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Move towards the player
        if (!moveToPlayer)
        {
            if (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
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
            Destroy(gameObject); // Destroy the orb
        }
    }
}
