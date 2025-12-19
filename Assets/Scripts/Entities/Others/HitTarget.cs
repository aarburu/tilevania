using System.Collections;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    CircleCollider2D StarCollider;
    Rigidbody2D StarRigidBody;
    private void Start()
    {
        StarCollider= GetComponent<CircleCollider2D>();
        StarRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StarRigidBody.linearVelocity = Vector3.zero;
            StarRigidBody.gravityScale = 0;

            StartCoroutine(DelayedDespawn());
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            var Enemy = collision.gameObject;
            var ReceiveDamageScript = Enemy.GetComponent<EnemyReceiveDamage>();
            if (ReceiveDamageScript != null)
            {
                ReceiveDamageScript.ReceiveDamage();
            }
            
            Destroy(this.gameObject);
        }
    }

    IEnumerator DelayedDespawn()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
