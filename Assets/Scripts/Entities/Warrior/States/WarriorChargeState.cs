public class WarriorChargeState : ProtagState
{
    public WarriorChargeState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.WARRIOR_CHARGE);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {
        // Logic for warrior charge state can be added here
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}