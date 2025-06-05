public class HunterRollState : ProtagState
{
    public HunterRollState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.HUNTER_ROLL);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {
        // Logic for the roll state can be added here if needed
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}