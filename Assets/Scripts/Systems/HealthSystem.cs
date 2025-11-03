using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Obsolete("Lógica separada entre UI / Controller para poder manejar la vida de los NPCs y el jugador de la misma forma, además de añadir soporte a ScriptableObjects.", true)]
public class HealthSystem : Singleton<HealthSystem>
{
    // Referencias UI. TODO:: Sacar a un HealthSystem para separar la lógica.
    //[field: SerializeField] TMP_Text HealthUIReference;
    [field: SerializeField] GameObject HealthBarUI;

    [field: SerializeField] GameObject heartPrefab;
    [field: SerializeField] Sprite FullHeartSprite;
    [field: SerializeField] Sprite EmptyHeartSprite;

    [field: SerializeField] private Color colourA = Color.red;
    [field: SerializeField] private Color colourB = Color.white;
    //[field: SerializeField] private Color DeadColour = Color.gray;

    [field: SerializeField] private float interval = 0.5f;
    private Coroutine flashRoutine;
    private List<GameObject> HeartList = new();

    static int MAX_INITIAL_HEALTH = 3;
    int CurrentMaxHealth { get; set; } = MAX_INITIAL_HEALTH;
    int CurrentHealth { get; set; } = MAX_INITIAL_HEALTH;
    public bool IsDead => CurrentHealth == 0;

    private void Start()
    {
        InitializeHearts(CurrentMaxHealth);
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

    public void IncreaseMaxHealth()
    {
        this.CurrentMaxHealth++;
        SetMaxLives(CurrentMaxHealth);
        CurrentHealth = CurrentMaxHealth;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //HealthUIReference.text = $"HP: {this.CurrentHealth}";
        UpdateHearts(CurrentHealth);
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
            HealthBarUI.transform.GetChild(0).GetComponent<Image>().color = colourB;
            //HealthUIReference.color = colourB;
        }
    }

    private IEnumerator FlashText()
    {
        bool toggle = false;
        while (true)
        {
            HealthBarUI.transform.GetChild(0).GetComponent<Image>().color = toggle ? colourA : colourB;
            //HealthUIReference.color = toggle ? colourA : colourB;
            toggle = !toggle;
            yield return new WaitForSeconds(interval);
        }
    }

    public void InitializeHearts(int maxLives)
    {
        for (int i = 0; i < maxLives; i++)
        {
            GameObject heartGO = Instantiate(heartPrefab, HealthBarUI.transform);
            HeartList.Add(heartGO);
        }
    }

    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < HeartList.Count; i++)
        {
            HeartList[i].GetComponent<Image>().sprite = i < currentLives ? FullHeartSprite : EmptyHeartSprite;
        }
    }

    public void SetMaxLives(int newMax)
    {
        int currentCount = HeartList.Count;

        if (newMax > currentCount)
        {
            for (int i = currentCount; i < newMax; i++)
            {
                GameObject heartGO = Instantiate(heartPrefab, HealthBarUI.transform);
                HeartList.Add(heartGO);
            }
        }
    }

}