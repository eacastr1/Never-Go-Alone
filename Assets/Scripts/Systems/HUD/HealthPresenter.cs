using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    public Image HeartPrefab;
    public Sprite FullHeartSprite;
    public Sprite HalfHeartSprite;
    public Sprite EmptyHeartSprite;

    private Health m_Model;
    private List<Image> m_Hearts = new List<Image>();

    public void UpdateView(float currentHealth)
    {
        UpdateHearts(currentHealth);
    }
    public void UpdateModel(Health model)
    {
        // Unsubscribe from the old model's event
        if (m_Model != null)
        {
            m_Model.OnHealthChanged -= UpdateView;
        }

        m_Model = model;

        // Subscribe to the new model's event
        if (m_Model != null)
        {
            m_Model.OnHealthChanged += UpdateView;
            // Reinitialize the view with the new model's max health
            InitializeHearts(m_Model.GetMaxHealth());
            // Update the view immediately to reflect the current health
            UpdateView(m_Model.GetCurrentHealth());
        }
    }
    public void Dispose()
    {
        m_Model.OnHealthChanged -= UpdateView;
    }

    private void InitializeHearts(float maxHearts)
    {
        foreach (Image heart in m_Hearts)
        {
            Destroy(heart.gameObject);
        }

        m_Hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(HeartPrefab, transform);
            newHeart.sprite = FullHeartSprite;
            // newHeart.color = Color.red;
            m_Hearts.Add(newHeart);
        }
    }
    private void UpdateHearts(float currentHealth)
    {
        float roundedHealth = RoundToNearestHalf(currentHealth);

        for (int i = 0; i < m_Hearts.Count; i++)
        {
            float heartValue = i + 1;

            if (heartValue <= roundedHealth)
            {
                m_Hearts[i].sprite = FullHeartSprite;
            }
            else if (heartValue - 0.5f == roundedHealth)
            {
                m_Hearts[i].sprite = HalfHeartSprite;
            }
            else
            {
                m_Hearts[i].sprite = EmptyHeartSprite;
            }
        }
    }
    private float RoundToNearestHalf(float value)
    {
        return Mathf.Round(value * 2f) / 2f;
    }
}