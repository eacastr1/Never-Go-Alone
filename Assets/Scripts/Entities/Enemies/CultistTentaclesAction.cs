using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Cultist Tentacles", story: "[Agent] attacks [Player]", category: "Action", id: "ac6f560b98b2d96d59017493716425ef")]
public partial class CultistTentaclesAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    private Antagonist m_Agent;
    private Ability m_Ability;

    protected override Status OnStart()
    {
        Agent.Value.TryGetComponent<Antagonist>(out m_Agent);
        m_Ability = m_Agent.AbilitySystem.GetAbility(AbilityNames.CULTIST_TWISTED_TENTACLES);

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

