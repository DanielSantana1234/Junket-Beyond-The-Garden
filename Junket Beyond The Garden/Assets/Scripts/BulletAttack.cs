using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit a Snake
        if (collision.CompareTag("Snake"))
        {
            Destroy(collision.gameObject); // destroy the enemy
            Destroy(gameObject);           // destroy the bullet
        }
        else if (collision.CompareTag("Wall")) // optional
        {
            Destroy(gameObject); // bullets disappear on walls
        }
    }
}
