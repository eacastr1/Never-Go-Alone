using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float m_CurrentHealth;
    private float m_MaxHealth;

    public event Action<float> OnHealthChanged;

    public void Awake()
    {
        m_MaxHealth = 3.5f;
        Initialize(m_MaxHealth);
    }

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

        OnHealthChanged?.Invoke(GetCurrentHealth());
        return m_CurrentHealth;
    }

    public float Heal(float amount)
    {
        m_CurrentHealth += amount;
        if (m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        
        OnHealthChanged?.Invoke(GetCurrentHealth());
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
