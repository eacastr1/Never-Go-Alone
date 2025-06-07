using System;

public class HealthPresenter
{
    private Health m_Model;
    private HealthView m_View;

    public HealthPresenter(Health model, HealthView view)
    {
        m_Model = model;
        m_View = view;

        // Subscribe to the model's health change event
        m_Model.OnHealthChanged += OnHealthChanged;

        // Initialize the view with max hearts (assuming model has MaxHealth)
        m_View.InitializeHearts(m_Model.GetMaxHealth());
        
        // Update the view immediately to reflect the current health
        UpdateView(m_Model.GetCurrentHealth());
    }

    private void OnHealthChanged(float newHealth)
    {
        UpdateView(newHealth);
    }

    public void UpdateView(float currentHealth)
    {
        m_View.UpdateHearts(currentHealth);
    }

    // Optional: Unsubscribe to avoid memory leaks if needed
    public void Dispose()
    {
        m_Model.OnHealthChanged -= OnHealthChanged;
    }
}