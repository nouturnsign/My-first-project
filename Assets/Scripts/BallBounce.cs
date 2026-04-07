using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private float bounceForce = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            rigidBody.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse);
    }
}
