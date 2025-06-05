using System.Collections.Generic;
using UnityEngine;


public class Hitbox : MonoBehaviour
{
    private Entity m_Owner;
    private List<IEffect> m_Effects;
    private HashSet<Entity> m_AlreadyHit;
    private Collider2D m_Collider;
    private ContactFilter2D m_Filter;
    public string Tag;


    public void Initialize(params IEffect[] effects)
    {
        m_Effects = new List<IEffect>(effects);
        m_AlreadyHit = new HashSet<Entity>();
    }

    public void SetTag(string tag)
    {
        Tag = tag;
    }

    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
        m_Filter = new ContactFilter2D();
        m_AlreadyHit = new HashSet<Entity>();
        // Flip();
    }

    private void FixedUpdate()
    {
        CheckOverlaps();
    }

    private void CheckOverlaps()
    {
        m_Filter.useLayerMask = true;
        m_Filter.layerMask = LayerMask.GetMask(Tag);
        Collider2D[] results = new Collider2D[10];
        int count = Physics2D.OverlapCollider(m_Collider, m_Filter, results);

        for (int i = 0; i < count; i++)
        {
            Entity entity = results[i].GetComponent<Entity>();
            if (entity != null && !m_AlreadyHit.Contains(entity))
            {
                m_AlreadyHit.Add(entity);

                Debug.Log($"Hitbox triggered by entity: {entity.name} with tag: {Tag}");
                foreach (var effect in m_Effects)
                {
                    effect.Effect(entity);
                    Debug.Log($"Applied effect: {effect.GetType().Name} to entity: {entity.name}");
                }
            }
        }
    }

    public void Flip(float direction)
    {
        if (direction > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1; // Flip horizontally
            transform.localScale = scale;
        }
        else if (direction < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1; // Flip horizontally
            transform.localScale = scale;
        }
    }
}