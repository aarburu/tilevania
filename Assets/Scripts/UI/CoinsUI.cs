using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text coinsText;

    private CoinController coinController;

    public void Bind(CoinController coinController)
    {
        // Unsubscribe from previous controller if exists
        if (this.coinController != null)
        {
            this.coinController.OnCoinsChanged -= UpdateCoins;
        }

        this.coinController = coinController;
        
        // Subscribe to new controller
        this.coinController.OnCoinsChanged += UpdateCoins;
        
        // Update UI immediately with current value
        UpdateCoins(this.coinController.Coins);
    }

    private void OnDestroy()
    {
        if (coinController != null)
        {
            coinController.OnCoinsChanged -= UpdateCoins;
        }
    }

    private void UpdateCoins(int current)
    {
        if (this == null) return;
        coinsText.text = current.ToString();
    }
}
