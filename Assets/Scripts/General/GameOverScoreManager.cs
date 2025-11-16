using TMPro;
using UnityEngine;

public class GameOverScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    void Start()
    {
        ScoreText.text = $"Score: {GameSession.Instance?.Coins.ToString()}";
        GameSession.Instance?.InitializeCoins();
    }
}
