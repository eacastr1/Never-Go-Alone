using System.Collections;
using UnityEngine;


public class WarriorChargeAbility : ProtagAbility
{
    public WarriorChargeAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {

    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown
        m_Protagonist.m_Performing = true; // Set performing to true to prevent other actions
        AbilityManager.Instance.StartCoroutine(Charge());
    }

    protected override void Deactivate()
    {

    }

    private IEnumerator Charge()
    {
        Vector2 originalPosition = m_Protagonist.transform.position;
        Vector2 targetPosition = new Vector2(
            originalPosition.x + (data.distance * m_Protagonist.Direction),
            originalPosition.y
        );

        m_Protagonist.m_Rooted = true;
        m_Protagonist.Rigidbody.linearVelocity = Vector2.zero;
        m_Protagonist.Animator.SetTrigger("Charge");

        float maxDuration = 1f; // Set a max time limit in seconds
        float elapsedTime = 0f;

        while (Vector2.Distance(m_Protagonist.Rigidbody.position, targetPosition) > 0.05f
               && elapsedTime < maxDuration)
        {
            Vector2 newPos = Vector2.MoveTowards(
                m_Protagonist.Rigidbody.position,
                targetPosition,
                data.speed * Time.fixedDeltaTime
            );

            m_Protagonist.Rigidbody.MovePosition(newPos);

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        m_Protagonist.m_Rooted = false;
        m_Protagonist.m_Performing = false;
    }
}