using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton genérico para Unity.
/// - Si existe manualmente en la escena, se mantiene al recargar la misma escena.
/// - Si se cambia a otra escena, se destruye el viejo y se crea uno nuevo.
/// - Usa DontDestroyOnLoad para persistir entre escenas.
/// </summary>
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
                // Busca si ya existe en la escena
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    // Si no existe, crea dinámicamente
                    GameObject singletonObj = new GameObject(typeof(T).Name);
                    _instance = singletonObj.AddComponent<T>();
                }

                // Guarda el nombre de la escena donde se creó
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

            // Desparentar antes de marcar como persistente
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // Si estamos en otra escena distinta, destruye el viejo
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
                // Si es la misma escena, destruye el duplicado
                Destroy(gameObject);
            }
        }
    }
}
