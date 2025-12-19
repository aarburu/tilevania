using UnityEngine;

public class CollectHeartContainer : MonoBehaviour
{
    Collider2D ItemCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip HeartCollectAudio;
    HealthController healthController;
    void Start()
    {
        ItemCollider = GetComponent<Collider2D>();
        var playerSetup = FindAnyObjectByType<PlayerSetup>();
        if (playerSetup != null)
        {
            healthController = playerSetup.GetComponent<HealthController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemCollider.IsTouchingLayers(playerLayer))
        {
            if (HeartCollectAudio  != null) 
                AudioSource.PlayClipAtPoint(HeartCollectAudio, transform.position);
            healthController.AddMaxHealth(1);
            Destroy(this.gameObject);
        }
    }
}