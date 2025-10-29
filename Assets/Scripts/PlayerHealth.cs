using System.Collections;
using System.Xml;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Referencias UI. TODO:: Sacar a un HealthSystem para separar la lógica.
    [field: SerializeField] TMP_Text HealthUIReference;

    [field: SerializeField] private Color colourA = Color.red;
    [field: SerializeField] private Color colourB = Color.white;
    [field: SerializeField] private Color DeadColour = Color.gray;

    [field: SerializeField] private float interval = 0.5f;
    private Coroutine flashRoutine;


    static int MAX_INITIAL_HEALTH = 3; 
    int CurrentMaxHealth { get; set; } = MAX_INITIAL_HEALTH; 
    int CurrentHealth { get; set; } = MAX_INITIAL_HEALTH;
    public bool IsDead => CurrentHealth == 0;

    private void Start()
    {
        UpdateUI();
    }

    public void ReceiveDamage()
    {
        this.CurrentHealth--;
        UpdateUI();
    }

    public void ReceiveLethalDamage()
    {
        this.CurrentHealth = 0;
        UpdateUI();
    }


    public void ReceiveHealing()
    {
        if (this.CurrentHealth < this.CurrentMaxHealth)
        {
            this.CurrentHealth++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        HealthUIReference.text = $"HP: {this.CurrentHealth}";
        ActivateLowHPEffect();
    }

    private void ActivateLowHPEffect()
    {
        if (CurrentHealth == 1)
        {
            StartFlashing();
        }
        else if (CurrentHealth > 1)
        {
            StopFlashing();
        }
        else
        {
            StopFlashing();
            HealthUIReference.color = DeadColour;
        }
    }

    public void StartFlashing()
    {
        if (flashRoutine == null)
            flashRoutine = StartCoroutine(FlashText());
    }

    public void StopFlashing()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
            HealthUIReference.color = colourB; 
        }
    }

    private IEnumerator FlashText()
    {
        bool toggle = false;
        while (true)
        {
            HealthUIReference.color = toggle ? colourA : colourB;
            toggle = !toggle;
            yield return new WaitForSeconds(interval);
        }
    }
}
