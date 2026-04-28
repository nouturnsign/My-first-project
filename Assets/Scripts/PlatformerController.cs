using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 3f;
    [SerializeField] private float teleportDisplacementX = 3f;
    [SerializeField] private float killPlaneY = 0f;

    private AudioSource audioSource;
    private Rigidbody2D rigidBody;
    private Vector2 initialPosition;
    private bool hasRemainingJump = false;
    private bool isGrounded = false;
    private bool hasTeleported = false;
    private bool isFacingRight = true;
    private float lastDirection = 1f;
    public int coinsCollected = 0;
    private Animator playerAnim;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialPosition = rigidBody.position;
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim.SetBool("isGrounded", isGrounded);
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
        if (vector.x != 0)
            lastDirection = Mathf.Sign(vector.x);

        playerAnim.SetBool("isRunning", rigidBody.linearVelocityX != 0f);
        if (vector.x < 0 && isFacingRight)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        if (vector.x > 0 && !isFacingRight)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
    }

    void OnJump()
    {
        Debug.Log($"{isGrounded}, {hasRemainingJump}");
        audioSource.clip = jumpSFX;

        if (isGrounded)
        {
            hasRemainingJump = true;
            rigidBody.linearVelocityY = jumpVelocity;
            audioSource.Play();
        }

        else if (hasRemainingJump)
        {
            hasRemainingJump = false;
            rigidBody.linearVelocityY = jumpVelocity;
            audioSource.Play();
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
