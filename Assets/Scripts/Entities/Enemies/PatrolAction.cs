using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Agent] patrols [Assignment]", category: "Action", id: "68225386a584dac8224327e7fa5ba35a")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<Antagonist> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Assignment;
    [SerializeReference] public BlackboardVariable<float> Speed = new(2f);
    [SerializeReference] public BlackboardVariable<float> StopDistance = new(0.1f);

    private List<Transform> m_WayPoints;

    private Vector2 m_TargetPosition;
    private bool m_HasTarget = false;

    protected override Status OnStart()
    {
        if (Assignment.Value == null || Agent.Value == null)
            return Status.Failure;

        m_WayPoints = Assignment.Value.GetComponentsInChildren<Transform>()
            .Where(t => t != Assignment.Value.transform)
            .ToList();

        var randomPoint = m_WayPoints[UnityEngine.Random.Range(0, m_WayPoints.Count)];
        m_TargetPosition = randomPoint.transform.position;
        m_HasTarget = true;

        return Status.Running;
    }
    protected override Status OnUpdate()
    {
        if (!m_HasTarget)
            return Status.Failure;

        var agent = Agent.Value;
        Vector2 currentPosition = agent.gameObject.transform.position;
        Vector2 targetPosition = m_TargetPosition;

        // ✅ Correct distance check
        if (Vector2.Distance(currentPosition, targetPosition) <= StopDistance.Value)
        {
            return Status.Success;
        }

        Vector2 toTarget = m_TargetPosition - currentPosition;
        Vector2 direction = toTarget.normalized;
        agent.m_Direction = direction;

        Vector2 move = currentPosition + direction * Speed.Value * Time.fixedDeltaTime;
        move.y = agent.Rigidbody.position.y; // In case of 2D physics
        agent.Rigidbody.MovePosition(move);

        // Debug.Log($"PatrolAction: {agent.gameObject.name} Moving towards target at {m_TargetPosition} with speed {Speed.Value}");

        return Status.Running;
    }

    protected override void OnEnd()
    {
        m_HasTarget = false;
    }
}

