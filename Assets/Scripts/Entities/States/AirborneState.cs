using UnityEngine;

public class AirborneState : ProtagState
{

    private Vector2 m_MoveDirection;

    public AirborneState(Protagonist protagonist) : base(protagonist)
    {

    }

    // move while airborne but movement is limited...lol...

    public override void OnEnter()
    {

    }

    public override void OnLogic()
    {
        if (m_Protagonist.m_Rooted)
        {
            Debug.Log("Protagonist is rooted, cannot move.");
            return;
        }
        
        m_MoveDirection = new Vector2(m_PlayerController.Input * 2.5f, m_Protagonist.Rigidbody.linearVelocityY);
        m_Protagonist.Direction = m_MoveDirection.x;
        m_Protagonist.Rigidbody.linearVelocity = m_MoveDirection;

        if (m_MoveDirection.x < 0)
        {
            m_Protagonist.SpriteRenderer.flipX = true;
        }
        else if (m_MoveDirection.x > 0)
        {
            m_Protagonist.SpriteRenderer.flipX = false;
        }

        /*
        if (m_Protagonist.Rigidbody.linearVelocityY > 0)
        {
            m_Protagonist.Animator.SetTrigger("Jump");
        }
        else
        {
            m_Protagonist.Animator.SetTrigger("Fall");
        }
        */
    }

    public override void OnExit()
    {

    }
}