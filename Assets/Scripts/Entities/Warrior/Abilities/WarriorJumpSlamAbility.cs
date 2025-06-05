using System.Collections;
using UnityEngine;

public class WarriorJumpSlamAbility : ProtagAbility
{
    Hitbox m_Hitbox;
    public WarriorJumpSlamAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        m_Hitbox = ((Warrior)protagonist).SlamHitbox.GetComponent<Hitbox>();
    }

    protected override void Activate()
    {
        if (Cooldown()) return;

        AbilityManager.Instance.StartCoroutine(JumpSlam());
    }

    protected override void Deactivate()
    {
        
    }

    private IEnumerator JumpSlam()
    {
        m_Hitbox.Initialize(new DamageEffect(data.damage), new KnockdownEffect(data.speed));
        m_Hitbox.SetTag("Enemy");

        m_Hitbox.Flip(m_Protagonist.Direction);

        m_Protagonist.Animator.SetTrigger("Ability One");

        m_Protagonist.m_Floating = true;

        yield return new WaitForSeconds(0.5f);

        m_Protagonist.m_Floating = false;

        yield break;
    }
}