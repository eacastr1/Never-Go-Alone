using UnityEngine;
using UnityHFSM;

public class Protagonist : Entity
{
    protected AbilitySystem m_AbilitySystem;

    // PROTAGONIST STATE MACHINES
    protected StateMachine m_StateMachine;
    protected StateMachine m_Status;

    // LOCOMOTION STATE MACHINES
    protected StateMachine m_Locomotion;
    protected StateMachine m_Grounded;
    protected StateMachine m_Airborne;

    // ACTION STATE MACHINES
    protected StateMachine m_Action;
    protected StateMachine m_GroundedAction;
    protected StateMachine m_AirborneAction;

    // PROTAGONIST COMPONENTS

    public AbilitySystem AbilitySystem => m_AbilitySystem;

    public bool m_Rooted; // Flag used to prevent movement during certain actions, like charging a crossbow
    public bool m_Floating; 
    public bool m_Performing; // Flag used to prevent actions during certain states, like when performing an ability

    protected override void Start()
    {
        m_AbilitySystem = GetComponent<AbilitySystem>();

        if (m_AbilitySystem == null)
        {
            Debug.LogError("AbilitySystem component is missing on the Protagonist.");
            return;
        }

        m_AbilitySystem.InitAbilities(m_Data.Abilities);

        // Initialize the state machines for the protagonist
        //m_Status = new StateMachine();
        m_Locomotion = new StateMachine();
        m_Action = new StateMachine();
        m_GroundedAction = new StateMachine();
        m_AirborneAction = new StateMachine();

        SetupStatusFSM();
        SetupLocomotionFSM();

        m_Action.AddState("Grounded", m_GroundedAction);
        m_Action.AddState("Airborne", m_AirborneAction);
        m_Action.AddTwoWayTransition("Grounded", "Airborne", t => !OnGround);

        m_GroundedAction.AddState("Idle", new State(isGhostState: true));
        m_AirborneAction.AddState("Idle", new State(isGhostState: true));

        SetupActionFSM();

        m_Action.SetStartState("Grounded");
        m_Action.Init();

        m_StateMachine = new StateMachine();

        m_StateMachine.AddState("Alive", new ParallelStates(
            m_Locomotion,
            m_Action
        ));

        m_StateMachine.AddState("Rooted", new RootedState(this));

        m_StateMachine.AddTwoWayTransition("Alive", "Rooted", t => m_Rooted);

        // m_StateMachine.AddState("Death")

        //m_StateMachine.AddState("Status", m_Status);
        //m_StateMachine.AddState("Locomotion", m_Locomotion);
        //m_StateMachine.AddState("Action", m_Action);


        m_StateMachine.SetStartState("Alive");
        m_StateMachine.Init();
    }

    protected virtual void Update()
    {
        m_StateMachine.OnLogic();
    }

    protected virtual void SetupStatusFSM()
    {
        // m_Status.AddState("Normal", new State(isGhostState: true));
    }

    protected virtual void SetupLocomotionFSM()
    {
        // Grounded HFSM
        m_Grounded = new StateMachine();
        m_Grounded.AddState("Idle", new State(isGhostState: true));
        m_Grounded.AddState("Move", new MoveState(this));
        m_Grounded.AddState("Jump", new JumpState(this));
        m_Grounded.AddTwoWayTransition("Idle", "Move", t => Player.Instance.PlayerController.Input != 0);
        m_Grounded.AddTwoWayTransition("Idle", "Jump", t => Player.Instance.PlayerController.AltInput.y > 0);
        m_Grounded.AddTwoWayTransition("Move", "Jump", t => Player.Instance.PlayerController.AltInput.y > 0);
        m_Grounded.SetStartState("Idle");
        m_Grounded.Init();


        // Airbone HFSM
        m_Airborne = new StateMachine();
        m_Airborne.AddState("Idle", new State(isGhostState: true));
        m_Airborne.AddState("Airborne", new AirborneState(this));
        m_Airborne.AddTwoWayTransition("Idle", "Airborne", t => Player.Instance.PlayerController.Input != 0);

        m_Airborne.AddState("Floating", new FloatingState(this));
        m_Airborne.AddTwoWayTransition("Airborne", "Floating", t => m_Floating);
        
        m_Airborne.AddTransition("Idle", "Floating", t => m_Floating);

        m_Airborne.SetStartState("Idle");
        m_Airborne.Init();


        m_Locomotion.AddState("Grounded", m_Grounded);
        m_Locomotion.AddState("Airborne", m_Airborne);
        m_Locomotion.AddTwoWayTransition("Grounded", "Airborne", t => !OnGround);

        m_Locomotion.SetStartState("Airborne");
        m_Locomotion.Init();
    }

    protected virtual void SetupActionFSM()
    {

    }
}
