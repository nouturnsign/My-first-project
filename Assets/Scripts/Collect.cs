using UnityEngine;

public class Collect : MonoBehaviour
{
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
