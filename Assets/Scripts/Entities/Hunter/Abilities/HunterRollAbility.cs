using System.Collections;
using UnityEngine;

public class HunterRollAbility : ProtagAbility
{
    public HunterRollAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        // Initialize any specific properties or states for the roll ability here
    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown

        m_Protagonist.Animator.SetTrigger("Defense");

        m_Protagonist.m_Performing = true; // Set performing to true to prevent other actions

        // Start the roll coroutine
        AbilityManager.Instance.StartCoroutine(Roll());

        Debug.Log("Hunter Roll Ability Activated");
    }

    protected override void Deactivate()
    {
        // Implement any cleanup or state reset logic if necessary
        Debug.Log("Hunter Roll Ability Deactivated");
    }

    private IEnumerator Roll()
    {
        Vector2 original = m_Protagonist.transform.position;
        Vector2 target = original + new Vector2(m_Protagonist.Direction * 2f, 0);
        float speed = 5f;

        Quaternion originalRotation = m_Protagonist.transform.rotation;

        BoxCollider2D collider = m_Protagonist.GetComponent<BoxCollider2D>();
        Vector2 originalColliderSize = collider.size;
        Vector2 originalOffset = collider.offset;

        // Only shrink the size, do not change the offset
        collider.size = new Vector2(originalColliderSize.x, 0.2f);
        collider.offset = new Vector2(originalOffset.x, -0.25f);

        m_Protagonist.OnGround = true;

        m_Protagonist.m_Rooted = true; // Root the protagonist during the roll

        // if (collider != null)
            //collider.enabled = false; // Disable for i-frames

        // bool colliderReenabled = false;

        while (Vector2.Distance(m_Protagonist.transform.position, target) > 0.1f)
        {
            m_Protagonist.Rigidbody.MovePosition(Vector2.MoveTowards(m_Protagonist.transform.position, target, speed * Time.fixedDeltaTime));

            RaycastHit2D hit = Physics2D.Raycast(
                m_Protagonist.transform.position,
                new Vector2(m_Protagonist.Direction, 0),
                0.25f,
                LayerMask.GetMask("Enemy", "Obstacle", "Ground", "Wall")
            );

            Debug.DrawLine(
                m_Protagonist.transform.position,
                m_Protagonist.transform.position + new Vector3(m_Protagonist.Direction * 0.25f, 0, 0),
                Color.red
            );

            if (hit.collider != null)
            {
                m_Protagonist.Animator.SetTrigger("Bump");
                collider.enabled = true;
                collider.size = originalColliderSize; // Reset collider size
                collider.offset = originalOffset; // Reset collider offset
                yield return new WaitForSeconds(0.25f);
                break;
            }

            float traveled = Vector2.Distance(original, m_Protagonist.transform.position);
            float totalDistance = Vector2.Distance(original, target);
            float t = Mathf.Clamp01(traveled / totalDistance);

            /*
            // Re-enable collider at 50% progress (i-frame ends)
            if (!colliderReenabled && t >= 0.5f)
            {
                if (collider != null)
                    collider.enabled = true;
                colliderReenabled = true;
            }
            */

            yield return new WaitForFixedUpdate();
        }

        /*
        // Just in case the loop exited before re-enabling
        if (!colliderReenabled && collider != null)
            collider.enabled = true;
        */

        m_Protagonist.transform.rotation = originalRotation;
        collider.size = originalColliderSize; // Reset collider size
        collider.offset = originalOffset; // Reset collider offset

        m_Protagonist.m_Rooted = false; // Unroot the protagonist after the roll
        m_Protagonist.m_Performing = false; // Reset performing state after the ability is activated
    }
}