using UnityEngine;
using UnityHFSM;

public class FallingState : ProtagState
{
    public FallingState(Protagonist protagonist) : base(protagonist)
    {

    }

    public override void OnEnter()
    {
        // m_Protagonist.Animator.SetBool("Falling", true);
        Debug.Log("Enter Falling State");
        m_Protagonist.Animator.SetTrigger("Fall");
    }


    public override void OnExit()
    {
        // m_Protagonist.Animator.SetBool("Falling", false);
    }
}