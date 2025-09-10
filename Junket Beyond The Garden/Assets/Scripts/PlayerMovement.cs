using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D characterBody;
    private Vector2 inputMovement;
    private Animator animator;
    private Vector2 lastMoveDirection;

    void Start()
    {
        characterBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();
    }

    void FixedUpdate()
    {
        // Move player with Rigidbody2D for smooth physics movement
        characterBody.MovePosition(characterBody.position + inputMovement * speed * Time.fixedDeltaTime);
    }

    private void ProcessInputs()
    {
        // WASD/Arrow input
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Save last direction when player moves
        if (inputMovement != Vector2.zero)
        {
            lastMoveDirection = inputMovement;
        }
    }

    private void Animate()
    {
        // Pass movement into Animator
        animator.SetFloat("MoveX", inputMovement.x);
        animator.SetFloat("MoveY", inputMovement.y);
        animator.SetFloat("Speed", inputMovement.sqrMagnitude);

        // Track last non-zero direction (so idle faces last moved direction)
        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }
}




