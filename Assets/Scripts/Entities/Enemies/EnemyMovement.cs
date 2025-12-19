using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    private Rigidbody2D rb;
    private EnemyReceiveDamage damageScript;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageScript = GetComponent<EnemyReceiveDamage>();
    }

    void Update()
    {
        if (damageScript != null && damageScript.IsHurting) return;
        rb.linearVelocity = transform.right * movementSpeed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageScript != null && damageScript.IsHurting) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            movementSpeed *= -1;
            FlipDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            movementSpeed *= -1;
            FlipDirection();
        }
    }


    private void FlipDirection()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
