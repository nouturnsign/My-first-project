using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 3f;
    private Rigidbody2D rigidBody;
    private bool isGrounded = false;
    public int coinsCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputValue value)
    {
        Vector2 vector = value.Get<Vector2>();
        rigidBody.linearVelocity = new Vector2(vector.x * speed, rigidBody.linearVelocity.y);
    }

    void OnJump()
    {
        if (isGrounded)
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
