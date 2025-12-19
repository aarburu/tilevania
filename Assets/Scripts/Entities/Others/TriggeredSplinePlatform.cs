using UnityEngine;

public class TriggeredSplinePlatform : SplinePlatform
{
    private bool hasBeenTriggered = false;

    protected override void FixedUpdate()
    {
        if (hasBeenTriggered)
        {
            base.FixedUpdate();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Player") && collision.collider is BoxCollider2D)
        {
            hasBeenTriggered = true;
        }
    }
}
