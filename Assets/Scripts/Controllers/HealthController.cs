using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HealthData healthConfig;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        MaxHealth = healthConfig.baseMaxHealth;
        CurrentHealth = MaxHealth;
    }

    public void ReceiveDamage(int amount = 1)
    {
        if (healthConfig.isImmortal || IsDead) return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (IsDead)
            OnDeath?.Invoke();
    }

    public void ReceiveLethalDamage()
    {
        if (healthConfig.isImmortal || IsDead) return;

        CurrentHealth = 0;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        OnDeath?.Invoke();
    }

    public void ReceiveHealing(int amount = 1)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
