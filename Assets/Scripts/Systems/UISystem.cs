//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class UISystem : Singleton<UISystem>
//{
//    //void Start()
//    //{
//    //    OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
//    //}
//    void OnEnable()
//    {
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    void OnDisable()
//    {
//        SceneManager.sceneLoaded -= OnSceneLoaded;
//    }

//    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        Debug.Log("1");

//        var playerSetup = FindAnyObjectByType<PlayerSetup>();
//        if (playerSetup != null)
//        {

//            Debug.Log("2");
//            var health = playerSetup.GetComponent<HealthController>();
//            var healthUI = GetComponentInChildren<HealthUI>();
//            healthUI.Bind(health);
//        }
//    }
//}
