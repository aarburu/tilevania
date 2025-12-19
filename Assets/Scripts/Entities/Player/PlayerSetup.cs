using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerSetup : MonoBehaviour
{
    //[SerializeField] private HealthUI healthUI;

    void Start()
    {
        Debug.Log("PlayerSetup: Start");
        //Provoco que se cargue el HealthController del jugador para hacer un bindeo con la UI.
        var healthController = GetComponent<HealthController>();
        var coinController = GetComponent<CoinController>();
        var healthUI = FindObjectsByType<HealthUI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Last();
        var coinsUI = FindObjectsByType<CoinsUI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Last();
        
        healthUI.Bind(healthController);
        coinsUI.Bind(coinController);
    }

}
