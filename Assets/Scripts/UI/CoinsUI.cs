using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text coinsText;

    public void Bind(CoinController coinController)
    {
        UpdateCoins(coinController.Coins);

        coinController.OnCoinsChanged += UpdateCoins;
    }

    private void UpdateCoins(int current)
    {
        coinsText.text = current.ToString();
    }
}
