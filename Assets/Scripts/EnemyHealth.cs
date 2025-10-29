using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // TODO:: Pasar a un ScriptableObject para añadir vidas de diferentes enemigos.
    static int MAX_INITIAL_HEALTH = 3; //Vida máxima inicial.
    int CurrentMaxHealth { get; set; } = MAX_INITIAL_HEALTH;
    public int CurrentHealth { get; set; } = MAX_INITIAL_HEALTH;
    public bool IsDead => CurrentHealth == 0;

    public void ReceiveDamage()
    {
        this.CurrentHealth--;
    }

    public void ReceiveLethalDamage()
    {
        this.CurrentHealth = 0;
    }


    public void ReceiveHealing()
    {
        if (this.CurrentHealth < this.CurrentMaxHealth)
            this.CurrentHealth++;
    }
}
