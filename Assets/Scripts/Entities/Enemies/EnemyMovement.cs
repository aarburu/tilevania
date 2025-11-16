using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //rb.linearVelocity = new Vector2(movementSpeed, rb.linearVelocity.y);
        rb.linearVelocity = transform.right * movementSpeed;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Star")) return; //Si se choca con una estrella, no debería darse la vuelta.
    //    movementSpeed *= -1;
    //    FlipDirection();
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
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
        //transform.localScale = new Vector2(-(Mathf.Sign(rb.linearVelocity.x)), 1f);
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
