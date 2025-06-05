using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Field of View", story: "[Player] in [Agent] Field of View", category: "Action", id: "a9bdbadcb95bd7f908e940b61c065a1a")]
public partial class FieldOfViewAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    private Antagonist m_Antagonist;
    private FieldOfView m_FOV;

    private bool m_TargetDetected;

    protected override Status OnStart()
    {
        Agent.Value.TryGetComponent<Antagonist>(out m_Antagonist);
        m_FOV = m_Antagonist?.FOV;


        if (m_Antagonist == null)
        {
            Debug.LogError("FieldOfViewAction: Agent is not an Antagonist or does not have an Antagonist component.");
            return Status.Failure;
        }
        if (m_FOV == null)
        {
            Debug.LogError("FieldOfViewAction: Agent does not have a FieldOfView component.");
            return Status.Failure;
        }

        m_FOV.OnTargetDetected += OnTargetDetected;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (m_TargetDetected)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        m_FOV.OnTargetDetected -= OnTargetDetected;
    }

    public void OnTargetDetected()
    {
        m_TargetDetected = true;
    }
}

