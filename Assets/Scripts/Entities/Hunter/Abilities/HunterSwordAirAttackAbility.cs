using System.Collections;
using UnityEngine;

public class HunterSwordAirAttackAbility : ProtagAbility
{
    private Hitbox m_Hitbox;
    public HunterSwordAirAttackAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        m_Hitbox = ((Hunter)protagonist).AirSwordHitbox.GetComponent<Hitbox>();
    }

    protected override void Activate()
    {
        if (Cooldown()) return;

        AbilityManager.Instance.StartCoroutine(SwordAirAttack());
    }

    protected override void Deactivate()
    {

    }

    private IEnumerator SwordAirAttack()
    {
        m_Hitbox.Initialize(new DamageEffect(data.damage), new StunEffect(data.duration));
        m_Hitbox.SetTag("Enemy");

        m_Protagonist.m_Floating = true;

        if (m_Protagonist.Direction > 0)
        {
            Vector3 scale = m_Hitbox.transform.localScale;
            scale.x = 1; // Flip horizontally
            m_Hitbox.transform.localScale = scale;
        }
        else if (m_Protagonist.Direction < 0)
        {
            Vector3 scale = m_Hitbox.transform.localScale;
            scale.x = -1; // Flip horizontally
            m_Hitbox.transform.localScale = scale;
        }

        m_Protagonist.Animator.SetTrigger("Ability Three");

        yield return new WaitForSeconds(0.35f);

        m_Protagonist.m_Floating = false;
    }
}