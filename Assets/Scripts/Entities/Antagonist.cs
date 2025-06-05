using Pathfinding;
using Unity.Behavior;
using UnityEngine;
public class Antagonist : Entity
{
    private BehaviorGraphAgent m_BehaviorGraphAgent;
    private AbilitySystem m_AbilitySystem;
    private FieldOfView m_FOV;

    private float m_LastDirection = 0f;
    private Vector2 m_LastPosition; // Add this field

    public BehaviorGraphAgent BehaviorGraphAgent => m_BehaviorGraphAgent;
    public AbilitySystem AbilitySystem => m_AbilitySystem;
    public FieldOfView FOV => m_FOV;
    public float LastDirection => m_LastDirection;

    protected override void Awake()
    {
        base.Awake();
        m_BehaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        m_AbilitySystem = GetComponent<AbilitySystem>();
        m_FOV = GetComponentInChildren<FieldOfView>();
    }

    protected override void Start()
    {
        base.Start();
        m_Direction = Vector2.right; // Default direction
        m_AbilitySystem.InitAbilities(m_Data.Abilities);
        m_LastPosition = transform.position; // Initialize last position
    }

    protected virtual void Update()
    {
        if (Health.GetCurrentHealth() <= 0)
        {
            m_BehaviorGraphAgent.BlackboardReference.SetVariableValue("IsDead", true);
            return;
        }

        /*
        // Only update if direction changed
        if (Mathf.Sign(Direction) != Mathf.Sign(m_LastDirection) && Direction != 0)
        {
            bool facingRight = Direction > 0;
            SpriteRenderer.flipX = facingRight;
            m_FOV.transform.rotation = Quaternion.Euler(0, 0, facingRight ? 0 : 180);
            m_LastDirection = Direction;
        }
        */

        if (m_Direction.x > 0)
        {
            SpriteRenderer.flipX = true; // Assuming true means facing right
            m_FOV.transform.rotation = Quaternion.Euler(0, 0, 0);
            m_LastDirection = Direction;
        }
        else if (m_Direction.x < 0)
        {
            SpriteRenderer.flipX = false; // Assuming false means facing left
            m_FOV.transform.rotation = Quaternion.Euler(0, 0, 180);
            m_LastDirection = Direction;
        }
    }

    protected virtual void FixedUpdate()
    {
        // Calculate displacement magnitude
        Vector2 currentPosition = transform.position;
        Magnitude = (currentPosition - m_LastPosition).magnitude / Time.fixedDeltaTime;
        m_LastPosition = currentPosition;

        Animator.SetFloat("Magnitude", Magnitude);
    }
}