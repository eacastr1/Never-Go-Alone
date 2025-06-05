using UnityEngine;
using UnityHFSM;

public class JumpState : ProtagState
{
    public float m_JumpForce = 6f;

    public JumpState(Protagonist protagonist) : base(protagonist)
    {

    }

    public override void OnEnter()
    {
        // Apply upward force
        m_Protagonist.Rigidbody.linearVelocity = new Vector2(
            m_Protagonist.Rigidbody.linearVelocity.x,
            m_JumpForce
        );

        m_Protagonist.Animator.SetTrigger("Jump");
    }

    public override void OnExit()
    {

    }
}