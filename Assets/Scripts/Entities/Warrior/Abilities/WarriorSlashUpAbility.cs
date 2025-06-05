using System.Collections;

public class WarriorSlashUpAbility : ProtagAbility
{
    private Hitbox m_Hitbox;
    public WarriorSlashUpAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        HitboxManager manager = ((Warrior)protagonist).SlashUpHitbox.GetComponent<HitboxManager>();
        m_Hitbox = manager.Hitbox;
        manager.SetOwner(protagonist);
    }

    protected override void Activate()
    {
        if (Cooldown()) return;
        
        AbilityManager.Instance.StartCoroutine(SlashUp());
    }

    protected override void Deactivate()
    {

    }

    private IEnumerator SlashUp()
    {
        m_Protagonist.Animator.SetTrigger("Ability Two");
        m_Hitbox.Initialize(new DamageEffect(data.damage), new KnockupEffect());
        m_Hitbox.SetTag("Enemy");
        yield return null;
    }
}