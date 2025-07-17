using UnityEngine;

public class HUDBootstrap : MonoBehaviour
{
    // Player model (?)
    private Player m_Player;

    // Health
    [SerializeField] private HealthPresenter m_HealthPresenter;
    // Portraits
    [SerializeField] private PortraitPresenter m_PortraitPresenter;

    private void Start()
    {
        m_Player = Player.Instance;
        m_Player.OnCharacterChanged += HUDSetup;
        HUDSetup();
    }

    private void OnDisable()
    {
        m_Player.OnCharacterChanged -= HUDSetup;
    }

    private void HUDSetup()
    {
        if (m_Player == null)
        {
            Debug.LogError("Player instance not found. Ensure Player is initialized before HUDBootstrap.");
            return;
        }

        // Initialize health presenter with player's health model
        Health playerHealth = m_Player.Entity.Health;
        if (playerHealth != null)
        {
            m_HealthPresenter.UpdateModel(playerHealth);
        }
        else
        {
            Debug.LogError("Player does not have a Health component.");
        }

        // Initialize portrait presenter with player's portraits
        if (m_PortraitPresenter != null)
        {
            m_PortraitPresenter.UpdatePortrait(m_Player.Entity.Portrait);
        }
        else
        {
            Debug.LogError("PortraitPresenter is not assigned in the inspector.");
        }
    }
}