using UnityEngine;

public abstract class ProtagAbility : Ability
{
    protected Protagonist m_Protagonist;
    protected PlayerController m_PlayerController;

    public ProtagAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        m_Protagonist = protagonist;
        m_PlayerController = Player.Instance.PlayerController;
    }
}