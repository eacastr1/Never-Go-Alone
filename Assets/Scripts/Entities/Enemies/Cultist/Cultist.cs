using UnityEngine;

public class Cultist : Antagonist
{
    public RuntimeAnimatorController CultistAnimatorController;
    public RuntimeAnimatorController TwistedAnimatorController;
    public CircleCollider2D CultistTargetRange;
    public CircleCollider2D TwistedTargetRange;
    public GameObject FireBallSpawn;
    public GameObject TentaclesHitbox;

    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        if (controller == null) return;
        ;
        if (Animator != null)
        {
            Animator.runtimeAnimatorController = controller;
        }
    }
}