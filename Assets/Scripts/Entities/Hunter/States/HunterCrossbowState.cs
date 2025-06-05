using UnityEngine;
using UnityHFSM;

public class HunterCrossbowState : ProtagState
{

    public HunterCrossbowState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.HUNTER_CROSSBOW);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
    }

    public override void OnLogic()
    {

    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}