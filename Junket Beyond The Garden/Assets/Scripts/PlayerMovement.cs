using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // use float for smoother movement
    private Rigidbody2D characterBody;
    private Vector2 inputMovement;

    void Start()
    {
        characterBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input (-1, 0, 1) for both axes
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized; // normalize so diagonal isn’t faster
    }

    void FixedUpdate()
    {
        // Movement
        Vector2 delta = inputMovement * speed * Time.fixedDeltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }
}





