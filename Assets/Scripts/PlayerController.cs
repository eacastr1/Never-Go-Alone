using UnityEngine;
using UnityHFSM;

public class PlayerController : MonoBehaviour
{
    private StateMachine m_ControllerState;

    private Entity m_Entity;
    private PlayerControls m_Controls;

    public float Input;
    public Vector2 AltInput; // Input for movement on both axes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Entity = Player.Instance.Entity;
        m_Controls = Player.Instance.PlayerControls;
    }

    // Update is called once per frame
    void Update()
    {
        Input = m_Controls.Movement.ReadValue<float>();
        AltInput = m_Controls.AltMovement.ReadValue<Vector2>();

        if (m_Controls.Switch.triggered)
        {
            Player.Instance.SwitchCharacter();
        }
    }
}
