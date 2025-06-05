public class HunterBombState : ProtagState
{
    public HunterBombState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.HUNTER_BOMB);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {
        // Logic for hunter bomb state can be added here
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}