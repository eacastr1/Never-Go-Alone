using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Agent] chases [Target]", category: "Action", id: "7a774313c89e83d410ebd88580f32d0b")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<Antagonist> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Coroutine _chaseCoroutine;
    private bool _reached;
    private bool _isDead;
    private bool _isHurt;

    protected override Status OnStart()
    {
        if (Agent?.Value == null || Target?.Value == null)
        {
            Debug.LogError("ChaseAction: Agent or Target is not set.");
            return Status.Failure;
        }

        _reached = false;
        _chaseCoroutine = Agent.Value.StartCoroutine(ChaseCoroutine());
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        /*
        if (Agent.Value.BehaviorGraphAgent.BlackboardReference.GetVariableValue<bool>("IsDead", out _isDead) ||
            Agent.Value.BehaviorGraphAgent.BlackboardReference.GetVariableValue<bool>("IsHurt", out _isHurt))
        {
            _reached = true; // Stop chasing if the agent is dead or hurt
            return Status.Failure;
        }
        */

        return _reached ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        if (_chaseCoroutine != null)
        {
            Agent.Value.StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }
    }
    private IEnumerator ChaseCoroutine()
    {
        var agent = Agent.Value;
        var targetTransform = Target.Value.transform;

        while (agent != null && targetTransform != null && Vector2.Distance(agent.transform.position, targetTransform.position) > 1.5f)
        {
            Vector2 currentPosition = agent.transform.position;
            Vector2 targetPosition = targetTransform.position;

            // Set direction for FOV/animations
            Vector2 direction = (targetPosition - currentPosition).normalized;
            agent.m_Direction = direction;

            // Move using physics
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, Speed.Value * Time.fixedDeltaTime);
            agent.Rigidbody.MovePosition(newPosition);

            yield return new WaitForFixedUpdate();
        }

        _reached = true;
    }
}