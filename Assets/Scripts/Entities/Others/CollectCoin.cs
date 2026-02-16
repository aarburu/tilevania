using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    Collider2D ItemCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip CoinCollectAudio;
    [SerializeField] CoinData CoinData;



    private void Start()
    {
        ItemCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ItemCollider.IsTouchingLayers(playerLayer))
        {
            CoinController coinController = other.GetComponent<CoinController>();
            if (coinController != null)
            {
                AudioSource.PlayClipAtPoint(CoinCollectAudio, transform.position);
                coinController.AddCoins(this.CoinData.CoinValue);
                Destroy(this.gameObject);
            }
        }
    }
}
