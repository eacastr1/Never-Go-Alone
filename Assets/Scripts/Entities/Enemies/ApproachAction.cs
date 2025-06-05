using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Pathfinding;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Approach", story: "[Agent] approaches [Target] until [Condition]", category: "Action", id: "a8d97ceeb93939625c679ced7c617bf1")]
public partial class ApproachAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<bool> Condition;
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Antagonist m_Agent;
    private AIPath m_AstarAgent;
    private AIDestinationSetter m_AstarDestinationSetter;
    private Transform m_TargetTransform;

    protected override Status OnStart()
    {
        // Set fields
        if (!Agent.Value.TryGetComponent<Antagonist>(out m_Agent))
        {
            LogFailure("Agent does not have an Antagonist component.");
            return Status.Failure;
        }

        m_TargetTransform = Target.Value?.transform;
        if (m_TargetTransform == null)
        {
            LogFailure("Target variable is not set or does not have a Transform component.");
            return Status.Failure;
        }

        return Status.Running;
    }
    protected override Status OnUpdate()
    {
        if (Condition.Value)
        {
            Debug.Log("ApproachAction: Condition met, stopping.");
            m_Agent.Rigidbody.linearVelocity = Vector2.zero;
            return Status.Success;
        }

        Vector2 direction = (m_TargetTransform.position - m_Agent.transform.position);
        direction.y = 0f; // Ignore Y movement
        direction = direction.normalized;

        // Preserve current Y velocity (e.g., for gravity)
        Vector2 currentVelocity = m_Agent.Rigidbody.linearVelocity;
        m_Agent.Rigidbody.linearVelocity = new Vector2(direction.x * Speed.Value, currentVelocity.y);

        float distance = Mathf.Abs(m_TargetTransform.position.x - m_Agent.transform.position.x);

        if (distance < 0.1f)
        {
            m_Agent.Rigidbody.linearVelocity = new Vector2(0f, currentVelocity.y);
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

