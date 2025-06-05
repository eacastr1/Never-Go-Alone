using UnityEngine;
using UnityHFSM;

public class Player : Entity
{
    public static Player Instance { get; private set; }

    [SerializeField] private Protagonist m_Entity;
    [SerializeField] private Protagonist m_CharacterOne;
    [SerializeField] private Protagonist m_CharacterTwo;

    private StateMachine m_EntityStateMachine;
    private StateMachine m_SupportStateMachine;

    private AbilitySystem m_AbilitySystem;
    private PlayerControls m_PlayerControls;
    private PlayerController m_PlayerController;

    public Protagonist Entity => m_Entity;
    public PlayerControls PlayerControls => m_PlayerControls;
    public PlayerController PlayerController => m_PlayerController;

    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        m_PlayerControls = GetComponent<PlayerControls>();
        m_PlayerController = GetComponent<PlayerController>();
        m_AbilitySystem = GetComponent<AbilitySystem>();
    }

    protected override void Start()
    {
        m_AbilitySystem.InitAbilities(m_Data.Abilities);
    }

    void Update()
    {
        transform.position = m_Entity.transform.position;
        Direction = m_Entity.Direction;

        if (PlayerControls.Assist.IsPressed())
        {
            Ability ability = m_AbilitySystem.GetAbility(AbilityNames.HUNTER_DASH_ATTACK_ASSIST);
            ability.TryActivate();
        }
    }

    public void SwitchCharacter()
    {
        if (m_Entity.m_Rooted)
            return;

        // switch from one character to another
        if (m_Entity == m_CharacterOne)
        {
            // Set old character inactive
            m_CharacterOne.gameObject.SetActive(false);
            // Update character position
            m_CharacterTwo.transform.position = transform.position;
            // Update Entity field
            m_Entity = m_CharacterTwo;
            // Preserve direction
            m_Entity.Direction = m_CharacterOne.Direction;
            //Set new character active
            m_CharacterTwo.gameObject.SetActive(true);
        }
        else
        {
            // Set old character inactive
            m_CharacterTwo.gameObject.SetActive(false);
            // Update character position
            m_CharacterOne.transform.position = transform.position;
            // Update Entity field
            m_Entity = m_CharacterOne;
            // Preserve direction
            m_Entity.Direction = m_CharacterTwo.Direction;
            // Set new character active
            m_CharacterOne.gameObject.SetActive(true);
        }

        if (m_Entity.Direction < 0)
        {
            m_Entity.SpriteRenderer.flipX = true;
        }
        else if (m_Entity.Direction > 0)
        {
            m_Entity.SpriteRenderer.flipX = false;
        }
    }

    private void SetupEntityStateMachine()
    {
        m_EntityStateMachine = new StateMachine();
        m_EntityStateMachine.AddState("Idle", new State(isGhostState: true));

        m_EntityStateMachine.AddState("Hunter");
        m_EntityStateMachine.AddState("Warrior");
    }

    private void SetupSupportStateMachine()
    {
        m_SupportStateMachine = new StateMachine();
        m_SupportStateMachine.AddState("Idle", new State(isGhostState: true));
    }

}
