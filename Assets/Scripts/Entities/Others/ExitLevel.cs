using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    Collider2D ItemCollider;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] float WaitSeconds = 3f;

    private void Start()
    {
        ItemCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ItemCollider.IsTouchingLayers(playerLayer))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(WaitSeconds);

        int SceneCount = SceneManager.sceneCountInBuildSettings;
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (CurrentSceneIndex < SceneCount - 1)
        {
            SceneManager.LoadScene(CurrentSceneIndex + 1);
        }
    }

}
