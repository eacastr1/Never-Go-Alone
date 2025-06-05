using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Line of Fire", story: "Is [Target] in [Agent] [LineOfFire]", category: "Action", id: "46000b7b94426760485ebe8918bfb89e")]
public partial class LineOfFireAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<bool> LineOfFire;

    private Antagonist m_Agent;
    Vector2 direction;
    Vector2 origin;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Target.Value == null)
        {
            LogFailure("Agent or Target variable is not set.");
            return Status.Failure;
        }

        if (!Agent.Value.TryGetComponent<Antagonist>(out m_Agent))
        {
            LogFailure("Agent does not have an Antagonist component.");
            return Status.Failure;
        }

        // Use LastDirection float to determine horizontal ray direction
        direction = new Vector2(Mathf.Sign(m_Agent.LastDirection), 0f);
        origin = Agent.Value.transform.position;

        // Optional: lengthen the visual ray for debugging
        // Debug.DrawRay(origin, direction * 10f, Color.red, 1f);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            Mathf.Infinity,
            LayerMask.GetMask("Obstacles", "Player")
        );

        if (hit.collider != null && hit.collider.gameObject == Target.Value)
        {
            LineOfFire.Value = true;
            return Status.Success;
        }

        LineOfFire.Value = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        direction = new Vector2(Mathf.Sign(m_Agent.LastDirection), 0f);
        origin = Agent.Value.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            Mathf.Infinity,
            LayerMask.GetMask("Obstacles", "Player")
        );

        Debug.DrawRay(origin, direction * 10f, Color.red, 0.1f);

        if (hit.collider != null && hit.collider.gameObject == Target.Value)
        {
            LineOfFire.Value = true;
            return Status.Success;
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

