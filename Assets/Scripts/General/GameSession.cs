using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : Singleton<GameSession>
{

     float sceneLoadDelay = 1f;
    public int PlayerLives { get; private set; }
    public int MaxHealth { get; private set; }
    public int MaxLives { get; private set; } = 99; //TODO:: Quitar el hardcodeo.
    public int CurrentHealth { get; private set; }

    LevelManager levelManager;

    public int Coins { get; private set; } = 0;
    protected override void Awake()
    {
        base.Awake();
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    public void InitializeHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void InitializeLives(int playerLives)
    {
        PlayerLives = playerLives;
    }
    public void InitializeCoins()
    {
        Coins = 0;
    }

    public void UpdateLives(int playerLives)
    {
        PlayerLives = Mathf.Clamp(playerLives, 0, MaxLives);
    }

    public void UpdateHealth(int current)
    {
        CurrentHealth = Mathf.Clamp(current, 0, MaxHealth);
    }

    public void ResetSession()
    {
        MaxHealth = 0;
        CurrentHealth = 0;
        InitializeCoins();
        levelManager.LoadGameOver();

        StartCoroutine(WaitAndLoad("_GameOver", sceneLoadDelay));
    }
    IEnumerator WaitAndLoad(string sceneName, float sceneLoadDelay)
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneName);
    }


    internal void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    internal void UpdateCoins(int Coins)
    {
        this.Coins = Coins;
    }
}
