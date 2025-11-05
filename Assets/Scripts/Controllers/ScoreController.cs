//using System;
//using UnityEngine;

//public class HealthController : MonoBehaviour
//{
//    [SerializeField] private HealthData healthConfig;
//    [SerializeField] private bool IsPlayer = false;

//    public int Score { get; private set; }
//    public int PlayerLives { get; private set; }
//    public int MaxHealth { get; private set; }
//    public int CurrentHealth { get; private set; }
//    public bool IsDead => CurrentHealth <= 0;


//    public event Action<int> OnLivesChanged;
//    public event Action<int, int> OnHealthChanged;
//    public event Action OnDeath;

//    private void Start()
//    {
//        MaxHealth = healthConfig.baseMaxHealth;

//        //En caso de que sea el jugador, obtenemos los datos de GameSession, si es que existen.
//        if (IsPlayer)
//        {
//            if (GameSession.Instance != null && GameSession.Instance.MaxHealth > 0)
//            {
//                MaxHealth = GameSession.Instance.MaxHealth;
//                CurrentHealth = GameSession.Instance.CurrentHealth;
//                PlayerLives = GameSession.Instance.PlayerLives;
//            }
//            else
//            {
//                CurrentHealth = MaxHealth;

//                GameSession.Instance?.InitializeHealth(MaxHealth);
//                GameSession.Instance?.InitializeLives(PlayerLives);
//            }
//        }

//        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
//    }

//    private void UpdateGameSessionHealth()
//    {
//        if (IsPlayer)
//            GameSession.Instance?.UpdateHealth(CurrentHealth);
//    }

//    public void ReceiveDamage(int amount = 1)
//    {
//        if (healthConfig.isImmortal || IsDead) return;

//        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
//        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

//        if (IsDead)
//            OnDeath?.Invoke();

//        UpdateGameSessionHealth();
//    }


//    public void ReceiveLethalDamage()
//    {
//        if (healthConfig.isImmortal || IsDead) return;

//        CurrentHealth = 0;
//        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
//        OnDeath?.Invoke();
//        UpdateGameSessionHealth();
//    }

//    public void ReceiveHealing(int amount = 1)
//    {
//        if (IsDead) return;

//        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
//        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
//        UpdateGameSessionHealth();
//    }

//    internal void ProcessPlayerDeath()
//    {
//        if (PlayerLives > 1)
//        {
//            TakeLife();
//        }
//        else
//        {
//            GameSession.Instance.ResetSession();
//        }
//    }

//    private void TakeLife()
//    {
//        PlayerLives--;
//        OnLivesChanged?.Invoke(PlayerLives);

//        GameSession.Instance.ReloadScene();
//    }
//}
