using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Hurt", story: "[Agent] is hurt", category: "Action", id: "2d94c6375bb0686f28421d1f8821e3d7")]
public partial class HurtAction : Action
{
    [SerializeReference] public BlackboardVariable<Antagonist> Agent;

    protected override Status OnStart()
    {
        Agent.Value.Animator.SetTrigger("Hurt");
        return Status.Success;
    }
}

