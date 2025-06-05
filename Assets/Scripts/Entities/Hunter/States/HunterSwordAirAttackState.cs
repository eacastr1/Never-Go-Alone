using UnityEngine;
using UnityHFSM;

public class HunterSwordAirAttackState : ProtagState
{
    public HunterSwordAirAttackState(Protagonist protagonist) : base(protagonist)
    {
        m_Ability = protagonist.AbilitySystem.GetAbility(AbilityNames.HUNTER_SWORD_AIR_ATTACK);
    }

    public override void OnEnter()
    {
        m_Ability.TryActivate();
        Debug.Log("Entering Air Attack");
    }

    public override void OnLogic()
    {
        // Logic for hunter sword air attack state can be added here
    }

    public override void OnExit()
    {
        m_Ability.TryDeactivate();
    }
}