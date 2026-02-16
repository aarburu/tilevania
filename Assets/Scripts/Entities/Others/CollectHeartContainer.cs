using UnityEngine;

public class CollectHeartContainer : MonoBehaviour
{
    Collider2D ItemCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip HeartCollectAudio;
    private void Start()
    {
        ItemCollider = GetComponent<Collider2D>();
    }

    // Correcting to use OnTriggerEnter2D for consistency and robustness
    private void OnTriggerEnter2D(Collider2D other)
    {
        Collect(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collect(collision.gameObject);
    }

    private void Collect(GameObject target)
    {
        if (target.GetComponent<PlayerSetup>() != null || target.GetComponent<HealthController>() != null)
        {
             HealthController healthController = target.GetComponent<HealthController>();
             if (healthController != null)
             {
                if (HeartCollectAudio  != null) 
                    AudioSource.PlayClipAtPoint(HeartCollectAudio, transform.position);
                healthController.AddMaxHealth(1);
                Destroy(this.gameObject);
             }
        }
    }
}