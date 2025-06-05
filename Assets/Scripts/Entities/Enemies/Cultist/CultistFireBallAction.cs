using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CultistFireBall", story: "[Agent] attacks [Target]", category: "Action", id: "69b906234ac91abf538f28397b13393c")]
public partial class CultistFireBallAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private Antagonist m_Agent;
    private Ability m_Ability;

    protected override Status OnStart()
    {
        Agent.Value.TryGetComponent<Antagonist>(out m_Agent);
        m_Ability = m_Agent.AbilitySystem.GetAbility(AbilityNames.CULTIST_FIREBALL);

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
        m_Ability.TryDeactivate();
    }
}

