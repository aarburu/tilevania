using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    Rigidbody2D rb;
    CapsuleCollider2D bodyCollider;
    PlayerHealth PlayerHealth;
    PertsonaiMugimendua PlayerControls;
    SpriteRenderer PlayerSprite;
    Animator PlayerAnimator;

    private bool InvulneravilityActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerControls = GetComponent<PertsonaiMugimendua>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        ReceiveDamageEffect();
    }

    public void ReceiveDamageEffect()
    {
        if (!PlayerHealth.IsDead)
        {
            if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")) && !InvulneravilityActive) { 
                ReceivePartialDamage();
            } else if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) {
                ReceiveLethalDamage();
            }
            if (PlayerHealth.IsDead)
            {
                PlayerControls.DisableControls();
                PlayerAnimator.SetTrigger("Death");
                rb.linearVelocity = deathKick;
            }
        }

    }

    private void ReceiveLethalDamage()
    {
        PlayerHealth.ReceiveLethalDamage();
    }

    private void ReceivePartialDamage()
    {
        InvulneravilityActive = true;
        PlayerHealth.ReceiveDamage();
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
