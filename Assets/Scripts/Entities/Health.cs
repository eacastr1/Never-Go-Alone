using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float m_CurrentHealth;
    private float m_MaxHealth;

    public void Initialize(float maxHealth)
    {
        m_MaxHealth = maxHealth;
        m_CurrentHealth = maxHealth;
    }

    public float TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;
        if (m_CurrentHealth < 0)
        {
            m_CurrentHealth = 0;
        }
        return m_CurrentHealth;
    }

    public float Heal(float amount)
    {
        m_CurrentHealth += amount;
        if (m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        return m_CurrentHealth;
    }

    public float GetCurrentHealth()
    {
        return m_CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return m_MaxHealth;
    }
}
