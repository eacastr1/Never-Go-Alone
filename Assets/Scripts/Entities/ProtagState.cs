using UnityEngine;
using UnityHFSM;

public class ProtagState : State
{
    protected Protagonist m_Protagonist;
    protected PlayerController m_PlayerController;
    protected Ability m_Ability;

    public ProtagState(Protagonist protagonist)
    {
        m_Protagonist = protagonist;
        m_PlayerController = Player.Instance.PlayerController;
    }
}
