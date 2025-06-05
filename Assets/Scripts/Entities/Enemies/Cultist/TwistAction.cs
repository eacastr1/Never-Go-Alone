using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Threading;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Twist", story: "[Agent] performs [Transformation] animation", category: "Action", id: "00b69ad650d1093107ce670f74e6e10f")]
public partial class TwistAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<CultistTransformations> Transformation;

    private Cultist m_Cultist;


    protected override Status OnStart()
    {
        m_Cultist = Agent.Value.GetComponent<Cultist>();

        if (m_Cultist == null)
        {
            LogFailure("Cultist variable is not set or does not have a Cultist component.");
            return Status.Failure;
        }

        m_Cultist.SetAnimatorController(Agent.Value.GetComponent<Cultist>().TwistedAnimatorController);

        return Status.Success;
   }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

