using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel()
    {
        // Destroy existing session to start fresh
        if (GameSession.Instance != null)
        {
            Destroy(GameSession.Instance.gameObject);
        }
        SceneManager.LoadScene("Level 1");
    }
    public void LoadMenuNagusia()
    {
        // Destroy existing session when going to menu
        if (GameSession.Instance != null)
        {
            Destroy(GameSession.Instance.gameObject);
        }
        SceneManager.LoadScene("_MainMenu");
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene("_GameOver");
    }
    public void QuitGame()
    {
        Debug.Log("Jokua bukatzen..."); 
        Application.Quit();
    }
}
