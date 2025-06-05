using System.Collections;
using UnityEngine;

public class KnockupEffect : IEffect
{
    private readonly float m_Height;
    private readonly float m_Speed;
    private readonly float m_Duration;

    /// <summary>
    /// Creates a new knockup effect.
    /// </summary>
    /// <param name="height">How high to knock the entity upward.</param>
    /// <param name="speed">Speed of the knockup motion.</param>
    /// <param name="duration">Failsafe timeout in seconds.</param>
    public KnockupEffect(float height = 2f, float speed = 5f, float duration = 0.5f)
    {
        m_Height = height;
        m_Speed = speed;
        m_Duration = duration;
    }

    public void Effect(Entity entity)
    {
        entity.StartCoroutine(Knockup(entity));
    }

    private IEnumerator Knockup(Entity entity)
    {
        Rigidbody2D rb = entity.Rigidbody;
        if (rb == null)
            yield break;

        float originalDrag = entity.Rigidbody.linearDamping;
        entity.Rigidbody.linearDamping = 5f; // or whatever feels good

        entity.Rigidbody.linearVelocity = Vector2.zero;
        entity.Rigidbody.AddForce(new Vector2(0, 1 * m_Speed), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.15f);

        entity.Rigidbody.linearDamping = originalDrag;

        // entity.m_Rooted = true; // Optional: freeze movement/input

        /*
        Vector2 startPosition = rb.position;
        Vector2 targetPosition = startPosition + Vector2.up * m_Height;

        float elapsedTime = 0f;

        while (Vector2.Distance(rb.position, targetPosition) > 0.05f && elapsedTime < m_Duration)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, m_Speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPosition); // Final snap
        // entity.m_Rooted = false;
        */
    }
}
