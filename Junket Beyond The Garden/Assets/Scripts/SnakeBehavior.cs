using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Look for player tagged "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Player' found in scene!");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Calculate direction to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move snake toward player using physics
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}