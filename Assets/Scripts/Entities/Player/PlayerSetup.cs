using System;
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
        var healthUI = FindFirstObjectByType<HealthUI>();
        var coinsUI = FindFirstObjectByType<CoinsUI>();
        
        healthUI.Bind(healthController);
        coinsUI.Bind(coinController);
    }
}
