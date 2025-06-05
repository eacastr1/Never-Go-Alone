public class AbilityState : ProtagState
{
    private new Ability m_Ability;

    public AbilityState(Protagonist protagonist, AbilityNames abilityName) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(abilityName);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();   
    }
}