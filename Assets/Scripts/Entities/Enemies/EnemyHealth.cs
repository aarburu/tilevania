using System;
using UnityEngine;

[Obsolete("Lógica separada entre UI / Controller para poder manejar la vida de los NPCs y el jugador de la misma forma, además de añadir soporte a ScriptableObjects.", true)]
public class EnemyHealth : MonoBehaviour
{
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
