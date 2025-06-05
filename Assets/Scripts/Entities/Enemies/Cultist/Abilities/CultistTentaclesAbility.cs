using UnityEngine;

public class CultistTentaclesAbility : AntagAbility
{
    Cultist m_Cultist;
    Hitbox m_TentaclesHitbox;
    // Hitbox
    public CultistTentaclesAbility(Antagonist antagonist, AbilityData data) : base(antagonist, data)
    {
        // Initialize the ability with the antagonist and data
        m_Cultist = antagonist as Cultist;
        m_Cultist.TentaclesHitbox.GetComponent<HitboxManager>().SetOwner(m_Cultist);
        m_TentaclesHitbox = m_Cultist.TentaclesHitbox.GetComponent<HitboxManager>().Hitbox;
    }

    protected override void Activate()
    {
        // Debug.Log("Cultist Tentacles Ability Activated");

        if (Cooldown()) return; // Check if the ability is on cooldown

        // Implement the logic to activate the tentacles ability
        // This could involve spawning tentacles, applying effects, etc.

        m_TentaclesHitbox.Initialize(new DamageEffect(data.damage), new KnockbackEffect(m_Cultist, m_Cultist.transform.position, data.distance));
        m_TentaclesHitbox.SetTag("Player");


        // m_Cultist.TentaclesHitbox.GetComponent<HitboxManager>().Hitbox.Initialize(new DamageEffect(data.damage));
        m_Antagonist.Animator.SetTrigger("Attack");
    }

    protected override void Deactivate()
    {
        // Implement the logic to deactivate the tentacles ability
        // This could involve stopping any ongoing effects, animations, etc.
    }
}