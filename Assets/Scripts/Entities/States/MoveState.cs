using System.Net.Sockets;
using UnityEngine;
using UnityHFSM;

public class MoveState : ProtagState
{
    private Vector2 m_MoveDirection;

    public MoveState(Protagonist protagonist) : base(protagonist)
    {

    }

    public override void OnLogic()
    {
        if (m_Protagonist.m_Rooted)
        {
            // If the protagonist is rooted, do not allow movement
            m_Protagonist.Animator.SetFloat("Magnitude", 0f);
            Debug.Log("Protagonist is rooted, cannot move.");
            return; // ✅ Exit early, do not run movement logic
        }

        m_MoveDirection = new Vector2(m_PlayerController.Input, 0);
        m_Protagonist.Direction = m_MoveDirection.x;
        m_Protagonist.Rigidbody.linearVelocity = m_MoveDirection * 2.5f;
        m_Protagonist.Animator.SetFloat("Magnitude", m_MoveDirection.magnitude);

        if (m_MoveDirection.x < 0)
        {
            m_Protagonist.SpriteRenderer.flipX = true;
        }
        else if (m_MoveDirection.x > 0)
        {
            m_Protagonist.SpriteRenderer.flipX = false;
        }
    }

    public override void OnExit()
    {

        // m_Protagonist.Rigidbody.linearVelocity = Vector2.zero;
        m_MoveDirection = Vector2.zero;
        m_Protagonist.Animator.SetFloat("Magnitude", m_MoveDirection.magnitude);
    }
}