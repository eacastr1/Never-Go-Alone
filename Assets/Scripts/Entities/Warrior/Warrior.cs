using UnityHFSM;
using UnityEngine;

public class Warrior : Protagonist
{
    public GameObject SlashDownHitbox;
    public GameObject SlashUpHitbox;
    public GameObject ChainSpawn;

    protected override void SetupActionFSM()
    {
        string ability = AbilityNames.WARRIOR_SWORD_SLASH_DOWN.ToString();
        m_GroundedAction.AddState(ability, new WarriorSlashDownState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", ability, t => Player.Instance.PlayerControls.AbilityOne.IsPressed());

        ability = AbilityNames.WARRIOR_SWORD_SLASH_UP.ToString();
        m_GroundedAction.AddState(ability, new WarriorSlashUpState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", ability, t => Player.Instance.PlayerControls.AbilityTwo.IsPressed());

        ability = AbilityNames.WARRIOR_CHARGE.ToString();
        m_GroundedAction.AddState(ability, new WarriorChargeState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", ability, t => Player.Instance.PlayerControls.AbilityThree.IsPressed());

        ability = AbilityNames.WARRIOR_CHAIN.ToString();
        m_GroundedAction.AddState(ability, new WarriorChainState(this));
        m_GroundedAction.AddTwoWayTransition("Idle", ability, t => Player.Instance.PlayerControls.AbilityFour.IsPressed());

        m_GroundedAction.SetStartState("Idle");
        m_GroundedAction.Init();

        m_AirborneAction.SetStartState("Idle");
        m_AirborneAction.Init();
    }
}