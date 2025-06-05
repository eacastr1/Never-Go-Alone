using System.Collections;
using UnityEngine;

public class KnockdownEffect : IEffect
{
    private float m_Speed;

    public KnockdownEffect(float speed)
    {
        m_Speed = speed;
    }

    public void Effect(Entity entity)
    {
        entity.StartCoroutine(Knockdown(entity));
    }

    private IEnumerator Knockdown(Entity entity)
    {
        if (entity.OnGround)
        {
            // do nothing (?)
            yield break;
        }

        // if the character is airborne, knock the character down
        Vector2 direction = new Vector2(0, -1);
        entity.Rigidbody.linearVelocity = Vector2.zero;
        entity.Rigidbody.AddForce(direction * m_Speed, ForceMode2D.Impulse);

        yield break;
    }
}