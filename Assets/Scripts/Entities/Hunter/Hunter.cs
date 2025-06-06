using UnityEngine;
using UnityHFSM;

public class Hunter : Protagonist
{
    public GameObject BoltArrowSpawn;
    public GameObject BombSpawn;
    public GameObject SwordHitbox;
    public GameObject AirSwordHitbox;

    protected override void SetupActionFSM()
    {
        Ability ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_CROSSBOW);
        m_GroundedAction.AddState(AbilityNames.HUNTER_CROSSBOW.ToString(), new HunterCrossbowState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_CROSSBOW.ToString(), t => Player.Instance.PlayerControls.AbilityOne.IsPressed());

        ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_CHARGED_CROSSBOW);
        m_GroundedAction.AddState(AbilityNames.HUNTER_CHARGED_CROSSBOW.ToString(), new HunterChargedCrossbowState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_CHARGED_CROSSBOW.ToString(), t => Player.Instance.PlayerControls.AbilityTwo.IsPressed());

        // Airborne action
        m_AirborneAction.AddState(AbilityNames.HUNTER_CHARGED_CROSSBOW.ToString(), new HunterChargedCrossbowState(this));
        m_AirborneAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_CHARGED_CROSSBOW.ToString(), t => Player.Instance.PlayerControls.AbilityTwo.IsPressed());

        ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_ROLL);
        m_GroundedAction.AddState(AbilityNames.HUNTER_ROLL.ToString(), new HunterRollState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_ROLL.ToString(), t => Player.Instance.PlayerControls.AbilityDefense.IsPressed());

        ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_SWORD_ATTACK);
        m_GroundedAction.AddState(AbilityNames.HUNTER_SWORD_ATTACK.ToString(), new HunterSwordAttackState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_SWORD_ATTACK.ToString(), t => Player.Instance.PlayerControls.AbilityThree.IsPressed() && OnGround);

        ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_BOMB);
        m_GroundedAction.AddState(AbilityNames.HUNTER_BOMB.ToString(), new HunterBombState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_BOMB.ToString(), t => Player.Instance.PlayerControls.AbilityFour.IsPressed());

        ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_SWORD_AIR_ATTACK);
        m_AirborneAction.AddState(AbilityNames.HUNTER_SWORD_AIR_ATTACK.ToString(), new HunterSwordAirAttackState(this));
        m_AirborneAction.AddTwoWayTransition("Idle", AbilityNames.HUNTER_SWORD_AIR_ATTACK.ToString(), t => Player.Instance.PlayerControls.AbilityThree.IsPressed());

        m_GroundedAction.SetStartState("Idle");
        m_GroundedAction.Init();

        m_AirborneAction.SetStartState("Idle");
        m_AirborneAction.Init();
    }
}