using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static string _sceneName;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject(typeof(T).Name);
                    _instance = singletonObj.AddComponent<T>();
                }

                _sceneName = SceneManager.GetActiveScene().name;
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (_instance == null)
        {
            _instance = this as T;
            _sceneName = currentScene;

            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            if (_sceneName != currentScene)
            {
                Destroy(_instance.gameObject);
                _instance = this as T;
                _sceneName = currentScene;

                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
