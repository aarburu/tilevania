using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;

    void Start()
    {
        //Provoco que se cargue el HealthController del jugador para hacer un bindeo con la UI.
        var health = GetComponent<HealthController>();
        healthUI.Bind(health);
    }
}
