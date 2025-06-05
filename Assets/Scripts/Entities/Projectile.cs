using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float m_TimeOutDelay = 5f;
    protected Animator m_Animator;
    protected IObjectPool<Projectile> m_ObjectPool;
    protected Rigidbody2D m_Rigidbody;
    protected SpriteRenderer m_SpriteRenderer;
    protected Entity m_Owner;

    public IObjectPool<Projectile> ObjectPool { set => m_ObjectPool = value; }
    public Rigidbody2D Rigidbody2D => m_Rigidbody;
    public SpriteRenderer SpriteRenderer => m_SpriteRenderer;
    public Vector2 Velocity;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + Velocity * Time.fixedDeltaTime);
    }

    public void Init(Entity owner)
    {
        m_Owner = owner;
        Deactivate(); // Start the timeout routine
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(m_TimeOutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.linearVelocity = Vector2.zero;
        rBody.angularVelocity = 0f;

        m_ObjectPool.Release(this);
    }
}