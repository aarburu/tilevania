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
        var IsTouchingGround = StarCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        var IsTouchingEnemy = StarCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"));
        if (IsTouchingGround || IsTouchingEnemy)
        {
            if (IsTouchingGround)
            {
                StarRigidBody.linearVelocity = Vector3.zero;
                StarRigidBody.gravityScale = 0;

                StartCoroutine(DelayedDespawn());
            }
            if (IsTouchingEnemy)
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
    }

    IEnumerator DelayedDespawn()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
