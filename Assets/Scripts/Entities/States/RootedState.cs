using UnityEngine;

public class RootedState : ProtagState
{
    // public float m_JumpForce = 6f;

    public RootedState(Protagonist protagonist) : base(protagonist)
    {

    }

    public override void OnEnter()
    {
        // m_Protagonist.Rigidbody.linearVelocity = new Vector2(0, 0);
        // m_Protagonist.Rigidbody.gravityScale = 0f;
        Debug.Log("Rooted");
    }

    public override void OnExit()
    {
        // m_Protagonist.Rigidbody.gravityScale = 1f;
    }
}
