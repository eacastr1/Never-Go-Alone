using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.Rendering.Universal;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CultistTransform", story: "[Cultist] decides on [Transformation]", category: "Action", id: "dbfcc2baf6d71efdcf1cae958dc0426f")]
public partial class CultistTransformAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Cultist;
    [SerializeReference] public BlackboardVariable<CultistTransformations> Transformation;
    private Cultist m_Cultist;

    protected override Status OnStart()
    {
        Cultist.Value.TryGetComponent(out m_Cultist);

        if (m_Cultist == null)
        {
            LogFailure("Cultist variable is not set or does not have a Cultist component.");
            return Status.Failure;
        }

        // Check Twisted Range
        Collider2D collider = Physics2D.OverlapCircle(m_Cultist.transform.position, m_Cultist.TwistedTargetRange.radius, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            // If a player is within the Twisted range, transform to Twisted
            Transformation.Value = CultistTransformations.Twisted;
            m_Cultist.SetAnimatorController(m_Cultist.TwistedAnimatorController);
            return Status.Success;
        }

        // Check Cultist Range
        collider = Physics2D.OverlapCircle(m_Cultist.transform.position, m_Cultist.CultistTargetRange.radius, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            // If a player is within the Cultist range, transform to Cultist
            Transformation.Value = CultistTransformations.Cultist;
            m_Cultist.SetAnimatorController(m_Cultist.CultistAnimatorController);
            return Status.Success;
        }

        return Status.Success; // No transformation needed, but still successful
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

