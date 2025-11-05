using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private TMP_Text livesText;

    [Header("Variables")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private float flashInterval = 0.5f;

    private List<GameObject> hearts = new();
    private Coroutine flashRoutine;

    public void Bind(HealthController health)
    {
        InitializeHearts(health.MaxHealth);
        UpdateHearts(health.CurrentHealth, health.MaxHealth);
        UpdateLives(health.PlayerLives);

        health.OnLivesChanged += UpdateLives;
        health.OnHealthChanged += UpdateHearts;
        health.OnDeath += StopFlashing;
    }

    private void InitializeHearts(int max)
    {
        for (int i = 0; i < max; i++)
        {
            var heart = Instantiate(heartPrefab, this.transform);
            hearts.Add(heart);
        }
    }

    private void UpdateHearts(int current, int max)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            var sprite = i < current ? fullHeart : emptyHeart;
            hearts[i].GetComponent<Image>().sprite = sprite;
        }

        if (current == 1)
            StartFlashing();
        else
            StopFlashing();
    }

    private void UpdateLives(int currentLives)
    {
        this.livesText.text = $"Lives: {currentLives.ToString()}";
    }

    private void StartFlashing()
    {
        if (flashRoutine == null)
            flashRoutine = StartCoroutine(Flash());
    }

    private void StopFlashing()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
            this.transform.GetChild(0).GetComponent<Image>().color = normalColor;
        }
    }

    private IEnumerator Flash()
    {
        bool toggle = false;
        while (true)
        {
            this.transform.GetChild(0).GetComponent<Image>().color = toggle ? flashColor : normalColor;
            toggle = !toggle;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
