public class HunterSwordAttackState : ProtagState
{
    public HunterSwordAttackState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.HUNTER_SWORD_ATTACK);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {
        // Logic for hunter sword attack state can be added here
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}