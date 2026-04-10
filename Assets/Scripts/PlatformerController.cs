using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 3f;
    [SerializeField] private float teleportDisplacementX = 3f;
    [SerializeField] private float killPlaneY = 0f;
    private Rigidbody2D rigidBody;
    private Vector2 initialPosition;
    private bool hasRemainingJump = false;
    private bool isGrounded = false;
    private bool hasTeleported = false;
    private float lastDirection = 1f;
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
            hasRemainingJump = false;
            isGrounded = false;
            hasTeleported = false;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 vector = value.Get<Vector2>();
        rigidBody.linearVelocityX = vector.x * speed;
        lastDirection = Mathf.Sign(vector.x);
    }

    void OnJump()
    {
        Debug.Log($"{isGrounded}, {hasRemainingJump}");

        if (isGrounded)
        {
            hasRemainingJump = true;
            rigidBody.linearVelocityY = jumpVelocity;
        }
        
        else if (hasRemainingJump)
        {
            hasRemainingJump = false;
            rigidBody.linearVelocityY = jumpVelocity;
        }
    }

    void OnTeleport()
    {
        if (!hasTeleported)
        {
            rigidBody.position = new Vector2(rigidBody.position.x + lastDirection * teleportDisplacementX, rigidBody.position.y);
            hasTeleported = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        hasTeleported = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        hasTeleported = false;
    }
}
