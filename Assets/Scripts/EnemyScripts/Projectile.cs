using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float destroyDelay = 2f;
    private float elapsedTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Disable gravity
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Set collision detection mode
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= destroyDelay)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");
            Destroy(gameObject);
        }
    }
}
