using System.Collections;
using UnityEngine;

public class HunterSwordAttackAbility : ProtagAbility
{
    Hitbox m_SwordHitbox;
    public HunterSwordAttackAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        HitboxManager manager = ((Hunter)protagonist).SwordHitbox.GetComponent<HitboxManager>();
        m_SwordHitbox = manager.Hitbox;
        manager.SetOwner(protagonist);
    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown

        // AbilityManager.Instance.StartCoroutine(SwordAttack());

        m_SwordHitbox.Initialize(new DamageEffect(data.damage));
        m_SwordHitbox.SetTag("Enemy");
        // For now, we will just trigger the sword attack animation
        m_Protagonist.Animator.SetTrigger("Ability Three");
        m_Protagonist.m_Rooted = true; // Root the protagonist during the sword attack animation
        AbilityManager.Instance.StartCoroutine(SwordAttack());
    }

    protected override void Deactivate()
    {
        // Logic for deactivating the sword attack ability can be added here if needed
    }

    private IEnumerator SwordAttack()
    {
        yield return new WaitForSeconds(0.45f);
        m_Protagonist.m_Rooted = false; // Unroot the protagonist after the sword attack
        m_Protagonist.m_Performing = false; // Reset performing state after the ability is activated
    }
}