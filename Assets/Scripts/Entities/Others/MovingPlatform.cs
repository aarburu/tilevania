using UnityEngine;
using UnityEngine.Splines;

public class SplinePlatform : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 2f;
    public float pauseDuration = 1f;
    public Transform attachPoint;

    protected Transform originalParent;
    protected float t = 0f;
    protected int direction = 1;
    protected bool isPaused = false;
    protected float pauseTimer = 0f;

    protected virtual void FixedUpdate()
    {
        // Pausa al llegar a un extremo
        if (isPaused)
        {
            pauseTimer += Time.fixedDeltaTime;
            if (pauseTimer >= pauseDuration)
            {
                isPaused = false;
                pauseTimer = 0f;
            }
            return;
        }

        // Anado la direccion para moverme hacia adelante o hacia atras
        t += direction * speed * Time.fixedDeltaTime / spline.CalculateLength();

        if (t >= 1f)
        {
            t = 1f;
            direction = -1;
            isPaused = true;
        }
        else if (t <= 0f)
        {
            t = 0f;
            direction = 1;
            isPaused = true;
        }

        Vector3 pos = spline.EvaluatePosition(t);
        transform.position = pos;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.collider is BoxCollider2D)
        {
            originalParent = collision.transform.parent;
            collision.transform.SetParent(attachPoint);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.collider is BoxCollider2D)
        {
            collision.transform.SetParent(originalParent);
        }
    }
}
