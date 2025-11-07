using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    Rigidbody2D rb;
    CapsuleCollider2D bodyCollider;
    HealthController healthController;
    PertsonaiMugimendua PlayerControls;
    SpriteRenderer PlayerSprite;
    Animator PlayerAnimator;

    private bool InvulneravilityActive = false;

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");

        if (healthController != null )
            healthController.OnDeath -= HealthController_OnDeath;

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        healthController = GetComponent<HealthController>();
        PlayerControls = GetComponent<PertsonaiMugimendua>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerAnimator = GetComponent<Animator>();
        healthController.OnDeath += HealthController_OnDeath;
    }

    private void Update()
    {
        ReceiveDamageEffect();
    }

    public void ReceiveDamageEffect()
    {
        if (!healthController.IsDead)
        {
            if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")) && !InvulneravilityActive) { 
                ReceivePartialDamage();
            } else if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) {
                ReceiveLethalDamage();
            }
            
        }

    }

    private void HealthController_OnDeath()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        rb.linearVelocity = deathKick;
        PlayerControls.DisableControls();
        PlayerAnimator.SetTrigger("Death");
        yield return new WaitForSecondsRealtime(3f);
        healthController.ProcessPlayerDeath();

    }

    private void ReceiveLethalDamage()
    {
        healthController.ReceiveLethalDamage();
    }

    private void ReceivePartialDamage()
    {
        InvulneravilityActive = true;
        healthController.ReceiveDamage();
        PlayerSprite.color = Color.red;
        StartCoroutine(DeactivateInvulnerability());
    }

    private IEnumerator DeactivateInvulnerability()
    {
        yield return new WaitForSeconds(2f);
        InvulneravilityActive = false;
        PlayerSprite.color = Color.white;
    }
}
