using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    Collider2D ItemCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip CoinCollectAudio;
    [SerializeField] CoinData CoinData;

    CoinController CoinController;

    private void Start()
    {
        ItemCollider = GetComponent<Collider2D>();

        var playerSetup = FindAnyObjectByType<PlayerSetup>();
        if (playerSetup != null)
        {
            CoinController = playerSetup.GetComponent<CoinController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ItemCollider.IsTouchingLayers(playerLayer))
        {
            AudioSource.PlayClipAtPoint(CoinCollectAudio, transform.position);
            CoinController.AddCoins(this.CoinData.CoinValue);
            Destroy(this.gameObject);
        }
    }
}
