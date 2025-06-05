using UnityEngine;

public abstract class Support : MonoBehaviour
{
    [SerializeField] private Entity m_Entity;
    [SerializeField] private GameObject m_SupportUI;
    private AbilitySystem m_AbilitySystem;
    private Animator m_Animator;


    public Entity Entity => m_Entity;
    public GameObject SupportUI => m_SupportUI;
    public AbilitySystem AbilitySystem => m_AbilitySystem;
    public Animator Animator => m_Animator;

    protected virtual void Awake()
    {
        if (m_Entity == null)
        {
            if (m_Entity == null)
            {
                Debug.LogError("Support requires an Entity to support.");
            }
        }

        m_AbilitySystem = GetComponent<AbilitySystem>();
        m_Animator = m_SupportUI.GetComponent<Animator>();
    }


}
