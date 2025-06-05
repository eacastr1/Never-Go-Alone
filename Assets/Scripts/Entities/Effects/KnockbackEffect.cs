using UnityEngine;
using System.Collections;

public class KnockbackEffect : IEffect
{
    private float m_Distance;
    private Entity m_Owner;


    public KnockbackEffect(Entity owner, Vector2 ownerPosition, float distance)
    {
        this.m_Owner = owner;
        this.m_Distance = distance;
    }

    public void Effect(Entity entity)
    {
        // apply knockback
        // entity.Rigidbody2D.AddForce(new Vector2(10f, 10f), ForceMode2D.Impulse);
        if (entity is Protagonist protagonist)
        {
            // Push the protagonist AWAY from the direction of the knockback
            protagonist.StartCoroutine(Knockback(protagonist));
        }
    }

    private IEnumerator Knockback(Protagonist entity)
    {
        entity.m_Rooted = true;
        float originalDrag = entity.Rigidbody.linearDamping;
        entity.Rigidbody.linearDamping = 5f; // or whatever feels good

        // Calculate direction away from owner
        Vector2 knockbackDirection = ((Vector2)entity.transform.position - (Vector2)m_Owner.transform.position).normalized;
        if (knockbackDirection == Vector2.zero)
            knockbackDirection = Vector2.right; // fallback

        // Apply impulse
        entity.Rigidbody.linearVelocity = Vector2.zero; // Reset velocity
        entity.Rigidbody.AddForce(new Vector2(Mathf.Sign(knockbackDirection.x), 0) * m_Distance, ForceMode2D.Impulse);

        // Wait for a short duration (let physics handle the movement)
        yield return new WaitForSeconds(0.2f);

        entity.Rigidbody.linearDamping = originalDrag;
        entity.m_Rooted = false;
    }
}