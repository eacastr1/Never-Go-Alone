public class WarriorSlashUpState : ProtagState
{
    public WarriorSlashUpState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.WARRIOR_SWORD_SLASH_UP);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {
        // Logic for warrior slash down state can be added here
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}