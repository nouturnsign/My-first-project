using UnityEngine;

public class Collect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlatformerController platformer = collider.GetComponent<PlatformerController>();
            platformer.coinsCollected++;
            Debug.Log(platformer.coinsCollected);
            Destroy(this.gameObject);
        }
    }
}
