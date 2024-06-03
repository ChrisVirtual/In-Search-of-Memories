using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject player;
    private float timer = 3;

    private bool moveToPlayer = false;
    private float speed = 3f;
    public Rigidbody2D rigidBody2D;
    public int goldAmount = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through it's tag
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //If the coin is not moving towards the player
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
            //Notify the game events manager that gold is gained
            GameEventsManager.instance.goldEvents.GoldGained(goldAmount);
            //Notify the game events manager that a coin is collected
            GameEventsManager.instance.miscEvents.coinCollected();
            //Destroy the coin
            Destroy(gameObject);
        }
    }
}
