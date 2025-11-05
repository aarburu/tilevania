using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : Singleton<GameSession>
{
    public int PlayerLives { get; private set; }
    public int MaxHealth { get; private set; }
    public int MaxLives { get; private set; } = 99; //TODO:: Quitar el hardcodeo.
    public int CurrentHealth { get; private set; }

    public void InitializeHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void InitializeLives(int playerLives)
    {
        PlayerLives = playerLives;
    }

    public void UpdateLives(int playerLives)
    {
        CurrentHealth = Mathf.Clamp(playerLives, 0, MaxLives);
    }

    public void UpdateHealth(int current)
    {
        CurrentHealth = Mathf.Clamp(current, 0, MaxHealth);
    }

    public void ResetSession()
    {
        MaxHealth = 0;
        CurrentHealth = 0;
        SceneManager.LoadScene(0);
    }

    internal void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
