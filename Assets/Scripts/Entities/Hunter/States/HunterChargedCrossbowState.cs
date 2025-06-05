using UnityEngine;
using UnityHFSM;

public class HunterChargedCrossbowState : ProtagState
{
    public HunterChargedCrossbowState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.HUNTER_CHARGED_CROSSBOW);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {
        // Logic for charged crossbow state can be added here
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}