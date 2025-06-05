using System.Collections;
using UnityEngine;
using UnityHFSM;

public class WarriorSlashDownAbility : ProtagAbility
{
    Hitbox m_Hitbox;

    public WarriorSlashDownAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        // Initialize the ability with the protagonist and data
        HitboxManager manager = ((Warrior)protagonist).SlashDownHitbox.GetComponent<HitboxManager>();
        m_Hitbox = manager.Hitbox;
        manager.SetOwner(protagonist);
    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown

        m_Protagonist.m_Performing = true; // Set performing to true to prevent other actions
        m_Protagonist.Animator.SetTrigger("Ability One");
        m_Hitbox.Initialize(new DamageEffect(data.damage));
        m_Hitbox.SetTag("Enemy"); // Set the hitbox tag to "Enemy" for collision detection

        m_Protagonist.m_Rooted = true; // Root the protagonist during the slash animation
        AbilityManager.Instance.StartCoroutine(SlashDown());
    }

    protected override void Deactivate()
    {
        // Implement any cleanup logic if necessary
    }

    private IEnumerator SlashDown()
    {
        // yield return new WaitForSeconds(0.4f); // Wait for the animation to finish

        // hitbox.Initialize(new DamageEffect(data.damage));

        yield return new WaitForSeconds(0.8f);

        m_Protagonist.m_Rooted = false; // Unroot the protagonist after slashing
        m_Protagonist.m_Performing = false; // Reset performing state after the ability is activated
        Debug.Log("Warrior Slash Down Ability Activated");
    }
}