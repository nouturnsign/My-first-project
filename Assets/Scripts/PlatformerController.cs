using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 3f;
    [SerializeField] private float killPlaneY = 0f;
    private Rigidbody2D rigidBody;
    private Vector2 initialPosition;
    private int remainingJumps = 0;
    private bool isGrounded = false;
    public int coinsCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialPosition = rigidBody.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (rigidBody.position.y < killPlaneY)
        {
            rigidBody.position = initialPosition;
            remainingJumps = 0;
            isGrounded = false;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 vector = value.Get<Vector2>();
        rigidBody.linearVelocityX = vector.x * speed;
    }

    void OnJump()
    {
        if (isGrounded || remainingJumps == 1)
        {
            remainingJumps--;
            rigidBody.linearVelocityY = jumpVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        remainingJumps = 2;
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
