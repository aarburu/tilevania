using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadMenuNagusia()
    {
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
