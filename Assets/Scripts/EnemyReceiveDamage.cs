using System.Collections;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{
    EnemyHealth EnemyHealth;
    Rigidbody2D rb;
    SpriteRenderer EnemySprite;
    CircleCollider2D CircleCollider;

    float OriginalPositionY;
    private void Start()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        CircleCollider = GetComponent<CircleCollider2D>();
        EnemySprite = GetComponent<SpriteRenderer>();
        OriginalPositionY = rb.position.y;
    }
    public void ReceiveDamage()
    {
        EnemyHealth.ReceiveDamage();
        StartCoroutine(DamageEffect());
        if (EnemyHealth.IsDead) {
            Destroy(this.gameObject);
        }
    }

    public void DamageKnockback()
    {
        CircleCollider.enabled = false;
        rb.linearVelocityY = 5f;
    }

    public void DamageColor()
    {
        EnemySprite.color = Color.red;
    }

    public IEnumerator RemoveDamageKnockback()
    {
        rb.linearVelocityY = -5f;
        yield return new WaitForSeconds(0.1f);
        rb.linearVelocityY = 0f;
        CircleCollider.enabled = true;

        //Aseguro que vuelva al suelo, a veces quedaba flotando en el aire.
        rb.position = new Vector2(rb.position.x, OriginalPositionY);
    }

    public void RemoveDamageColor()
    {
        EnemySprite.color = Color.white;
    }

    public IEnumerator DamageEffect()
    {
        DamageKnockback();
        DamageColor();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(RemoveDamageKnockback());
        RemoveDamageColor();
    }
}