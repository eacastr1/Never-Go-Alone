using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Agent] attacks with [Ability]", category: "Action", id: "0dcd5f7c773622947668d315c253bc53")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Antagonist> Agent;
    [SerializeReference] public BlackboardVariable<AbilityNames> Ability;
    private Ability m_Ability;

    protected override Status OnStart()
    {
        m_Ability = Agent.Value.AbilitySystem.GetAbility(Ability.Value);

        if (m_Ability == null)
        {
            Debug.LogError("CultistFireBallAction: Ability not found.");
            return Status.Failure;
        }

        m_Ability.TryActivate();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

