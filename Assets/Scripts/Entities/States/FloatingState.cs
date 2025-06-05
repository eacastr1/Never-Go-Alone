using UnityEngine;

public class FloatingState : ProtagState
{
    public FloatingState(Protagonist protagonist) : base(protagonist)
    {

    }

    public override void OnEnter()
    {
        m_Protagonist.Rigidbody.gravityScale = 0;
        m_Protagonist.Rigidbody.linearVelocity = Vector2.zero;
    }

    public override void OnExit()
    {
        m_Protagonist.Rigidbody.gravityScale = 1;
    }
}