using UnityEngine;
using System.Collections;

public class GrappleEffect : IEffect
{
    private Entity m_Owner;
    private Vector2 m_Position;
    private float m_Speed;
    private float m_Time;

    public GrappleEffect(Entity owner, Vector2 position, float speed, float time)
    {
        m_Owner = owner;
        m_Position = position;
        m_Speed = speed;
        m_Time = time;
    }


    public void Effect(Entity entity)
    {
        m_Owner.StartCoroutine(Grapple(entity));
    }

    private IEnumerator Grapple(Entity entity)
    {
        Rigidbody2D rb = entity.Rigidbody;
        if (rb == null)
            yield break;

        yield return new WaitForSeconds(m_Time);

        Transform ownerTransform = m_Owner.transform;
        Vector2 targetDir = (ownerTransform.position - entity.transform.position).normalized;

        // Set a fixed offset (e.g. 0.5 units away from the owner)
        float offsetDistance = 0.5f;
        Vector2 targetPosition = (Vector2)ownerTransform.position - targetDir * offsetDistance;

        float maxDuration = 1f; // seconds
        float elapsedTime = 0f;

        while (Vector2.Distance(rb.position, targetPosition) > 0.1f && elapsedTime < maxDuration)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, m_Speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate(); // Use FixedUpdate timing for physics
        }

        rb.MovePosition(targetPosition); // Optional final snap
    }
}