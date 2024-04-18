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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //gets the player reference through it's tag
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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
            Vector3 movementVector = player.transform.position - transform.position;
            rigidBody2D.velocity = movementVector * speed;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if exp orb touches player
        {
           GameEventsManager.instance.goldEvents.GoldGained(goldAmount);
            Destroy(gameObject); //Destroy the orb
        }
    }
}
