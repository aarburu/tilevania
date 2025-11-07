using System;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int Coins { get; private set; }
    public Action<int> OnCoinsChanged { get; internal set; } 

    private void Start()
    {
        if (GameSession.Instance != null && GameSession.Instance.MaxHealth > 0)
        {
            Coins = GameSession.Instance.Coins;
        }
        else
        {
            Coins = 0;
            GameSession.Instance?.InitializeCoins();
        }
    }


    private void UpdateGameSessionCoins()
    {
        GameSession.Instance?.UpdateCoins(Coins);
    }

    public void AddCoins(int coinValue)
    {
        Coins += coinValue;
        OnCoinsChanged?.Invoke(Coins);
        UpdateGameSessionCoins();
    }

    public void RemoveCoins(int coins)
    {
        Coins -= coins;
        OnCoinsChanged?.Invoke(Coins);
        UpdateGameSessionCoins();
    }
}
