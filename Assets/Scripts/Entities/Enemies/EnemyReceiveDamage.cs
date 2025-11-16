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
        ApplyKnockback();
        ApplyDamageColor();

        yield return new WaitForSeconds(knockbackDuration);

        ResetKnockback();
        ResetDamageColor();
    }

    private void ApplyKnockback()
    {
        originalKnockbackPosition = transform.position;
        circleCollider.enabled = false;
        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, knockbackForce);
        rb.linearVelocity = transform.up * knockbackForce;

    }

    private void ResetKnockback()
    {
        rb.linearVelocity = Vector2.zero;
        circleCollider.enabled = true;
        rb.position = originalKnockbackPosition; // Devuelvo el sprite a la posicion Y original para evitar que quede flotan
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