using UnityEngine;
using UnityHFSM;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityData m_Data;
    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_BoxCollider;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private Health m_Health;

    public Rigidbody2D Rigidbody => m_Rigidbody;
    public BoxCollider2D BoxCollider => m_BoxCollider;
    public Animator Animator => m_Animator;
    public SpriteRenderer SpriteRenderer => m_SpriteRenderer;
    public Health Health => m_Health;

    public Vector2 m_Direction;
    public bool OnGround;
    public float Direction;
    public float Magnitude;

    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_Animator = GetComponentInChildren<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Health = GetComponent<Health>();
        m_Health.Initialize(100f); // Example initialization, adjust as needed
    }

    protected virtual void Start()
    {
        
    }

    public void Death()
    {
        m_Animator.SetTrigger("Death");
        // m_Rigidbody.simulated = false;
        // m_BoxCollider.enabled = false;
    }

    public void Flip()
    {
        if (m_SpriteRenderer != null)
        {
            m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnGround = true;
        m_Animator.SetBool("On Ground", OnGround);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnGround = false;
        m_Animator.SetBool("On Ground", OnGround);
    }
}
