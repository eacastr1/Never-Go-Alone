using System.Collections;
using UnityEngine;

public class StunEffect : IEffect
{
    private float m_Duration;

    public StunEffect(float duration)
    {
        this.m_Duration = duration;
    }

    public void Effect(Entity entity)
    {
        // entity goes to stun state
        if (entity is Antagonist antagonist)
        {
            antagonist.BehaviorGraphAgent.BlackboardReference.SetVariableValue<bool>("IsStunned", true);
            antagonist.BehaviorGraphAgent.Restart();
        }

        // apply stun 
        entity.StartCoroutine(Stun(entity));
    }

    private IEnumerator Stun(Entity entity)
    {
        // disable gravity
        entity.Rigidbody.gravityScale = 0f;
        // set linear velocity to 0
        entity.Rigidbody.linearVelocityX = 0f;
        entity.Rigidbody.linearVelocityY = 0f;
        // wait a certain amount of time
        yield return new WaitForSeconds(1f);
        // reenable gravity
        entity.Rigidbody.gravityScale = 1f;
    }
}