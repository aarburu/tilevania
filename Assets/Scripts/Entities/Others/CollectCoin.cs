using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    Collider2D ItemCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip CoinCollectAudio;

    private void Start()
    {
        ItemCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ItemCollider.IsTouchingLayers(playerLayer))
        {
            AudioSource.PlayClipAtPoint(CoinCollectAudio, transform.position);
            Destroy(this.gameObject);
        }
    }
}
