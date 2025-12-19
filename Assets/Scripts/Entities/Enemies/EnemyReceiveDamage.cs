using System.Collections;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;
    
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.1f;

    private HealthController health;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Vector2 originalKnockbackPosition;
    
    public bool IsHurting { get; private set; }

    private void Awake()
    {
        health = GetComponent<HealthController>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void ReceiveDamage()
    {
        health.ReceiveDamage();
        StartCoroutine(PlayDamageEffects());

        if (health.IsDead)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayDamageEffects()
    {
        // Knockback up
        ApplyKnockback();
        ApplyDamageColor();

        yield return new WaitForSeconds(knockbackDuration);

        // Knockback down
        ApplyKnockbackDown();
        yield return new WaitForSeconds(knockbackDuration);

        // Reset del knockback para asegurar que se queda al mismo nivel que al inicio.
        ResetKnockback();
        ResetDamageColor();
    }

    private void ApplyKnockbackDown()
    {
        rb.linearVelocity = -transform.up * knockbackForce;
    }

    private void ApplyKnockback()
    {
        IsHurting = true;
        originalKnockbackPosition = transform.position;
        rb.linearVelocity = transform.up * knockbackForce;

    }

    private void ResetKnockback()
    {
        rb.linearVelocity = Vector2.zero;
        rb.position = originalKnockbackPosition; 
        IsHurting = false;
    }

    private void ApplyDamageColor()
    {
        spriteRenderer.color = damageColor;
    }

    private void ResetDamageColor()
    {
        spriteRenderer.color = normalColor;
    }
}