using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    public InputAction Movement;
    public InputAction AltMovement; // This action is not used in the current code but is defined in the Controls class
    public InputAction AbilityOne;
    public InputAction AbilityTwo;
    public InputAction AbilityThree;
    public InputAction AbilityFour;
    public InputAction AbilityDefense;
    public InputAction Spell;
    public InputAction Switch;
    public InputAction Assist;
    private Controls m_Controls;

    void Awake()
    {
        m_Controls = new Controls();

        Movement = m_Controls.General.Movement;
        AbilityOne = m_Controls.General.AbilityOne;
        AbilityTwo = m_Controls.General.AbilityTwo;
        AbilityThree = m_Controls.General.AbilityThree;
        AbilityFour = m_Controls.General.AbilityFour;
        AbilityDefense = m_Controls.General.AbilityDefense;
        Spell = m_Controls.General.Spell;
        Switch = m_Controls.General.Switch;
        Assist = m_Controls.General.Assist;

        AltMovement = m_Controls.Alternate.Movement;
    }

    void OnEnable()
    {
        Movement.Enable();
        AbilityOne.Enable();
        AbilityTwo.Enable();
        AbilityThree.Enable();
        AbilityFour.Enable();
        AbilityDefense.Enable();
        Spell.Enable();
        Switch.Enable();
        Assist.Enable();

        AltMovement.Enable();
    }

    void OnDisable()
    {
        Movement.Disable();
        AbilityOne.Disable();
        AbilityTwo.Disable();
        AbilityThree.Disable();
        AbilityFour.Disable();
        AbilityDefense.Disable();
        Spell.Disable();
        Switch.Disable();
        Assist.Disable();

        AltMovement.Disable();
    }
}
