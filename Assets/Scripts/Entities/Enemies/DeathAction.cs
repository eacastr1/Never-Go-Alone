using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Death", story: "[Agent] dies", category: "Action", id: "416640719b64242307f8888308929aeb")]
public partial class DeathAction : Action
{
    [SerializeReference] public BlackboardVariable<Antagonist> Agent;
    
    protected override Status OnStart()
    {
        Debug.Log($"DeathAction: {Agent.Value.name} is dying.");
        Agent.Value.Death();
        Agent.Value.BehaviorGraphAgent.End();
        return Status.Success;
    }
}

