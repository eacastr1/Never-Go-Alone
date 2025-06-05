using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Line of Sight", story: "Is Player in [Agent] line of sight", category: "Action", id: "fd0f3c4ddb44429370085ce71bfcafb1")]
public partial class LineOfSightAction : Action
{
    [SerializeReference] public BlackboardVariable<Antagonist> Agent;
    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            Debug.LogError("LineOfSightAction: Agent is null.");
            return Status.Failure;
        }

        Vector2 origin = Agent.Value.transform.position;
        Vector2 direction = Agent.Value.m_Direction.normalized;
        direction.y = 0; // Ensure direction is horizontal

        if (direction == Vector2.zero)
        {
            Debug.LogWarning("LineOfSightAction: Agent direction is zero; can't check vision.");
            return Status.Failure;
        }

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            Mathf.Infinity,
            LayerMask.GetMask("Player")
        );

        Debug.DrawRay(origin, direction * 10f, Color.red, 0.1f);

        if (hit.collider != null)
        {
            return Status.Success;
        }

        return Status.Failure;
    }
}

