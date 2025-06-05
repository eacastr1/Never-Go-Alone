public class DamageEffect : IEffect
{
    private float m_Damage;

    public DamageEffect(float damage)
    {
        this.m_Damage = damage;
    }

    public void Effect(Entity entity)
    {
        // entity take damage
        // entity.TakeDamage(damage);
        entity.Health.TakeDamage(m_Damage);

        if (entity.Health.GetCurrentHealth() > 0)
        {
            entity.Animator.SetTrigger("Hurt");
        }

        if (entity is Antagonist antagonist)
        {
            // If the entity is an Antagonist, we can add additional effects or logic here
            antagonist.BehaviorGraphAgent.BlackboardReference.SetVariableValue("IsHurt", true);
            antagonist.BehaviorGraphAgent.Restart();
        }
    }
}