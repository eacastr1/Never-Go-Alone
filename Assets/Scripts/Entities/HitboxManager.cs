using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    public GameObject HitboxParent;
    public Hitbox Hitbox;
    public float Direction;
    public Entity owner;

    public void Awake()
    {
        // Hitbox = GetComponentInChildren<Hitbox>();
        if (Hitbox == null)
        {
            Debug.LogError("Hitbox component not found in children.");
        }
    }

    public void SetDirection(float direction)
    {
        Direction = direction;
    }

    public void SetOwner(Entity entity)
    {
        owner = entity;
    }

    public void SetHitboxDirection()
    {
        if (HitboxParent != null)
        {
            HitboxParent.transform.rotation = Quaternion.Euler(0, 0, owner.m_Direction.x > 0 ? 0 : 180);
        }
        else
        {
            Debug.LogError("Hitbox Owner is not initialized.");
        }
    }

    public void EnableHitbox()
    {
        Hitbox.gameObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        Hitbox.gameObject.SetActive(false);
    }
}