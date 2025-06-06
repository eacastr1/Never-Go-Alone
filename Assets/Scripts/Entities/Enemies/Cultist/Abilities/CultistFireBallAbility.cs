using System.Collections;
using UnityEngine;

public class CultistFireBallAbility : AntagAbility
{
    private Projectile m_Prefab;
    private ProjectilePoolManager m_PoolManager;

    public CultistFireBallAbility(Antagonist antagonist, AbilityData data) : base(antagonist, data)
    {
        m_Prefab = data.prefab.GetComponent<Projectile>();
        m_PoolManager = new ProjectilePoolManager(m_Prefab);
    }

    protected override void Activate()
    {
        // Debug.Log("Cultist Fireball Ability Activated");

        if (Cooldown()) return; // Check if the ability is on cooldown

        AbilityManager.Instance.StartCoroutine(Fire());
    }

    protected override void Deactivate()
    {
        // Implement the logic to deactivate the fireball ability
        // This could involve stopping any ongoing effects, animations, etc.
    }

    // Fix later ???
    private IEnumerator Fire()
    {
        m_Antagonist.Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // Wait for the animation to finish

        Transform spawnTransform = ((Cultist)m_Antagonist).FireBallSpawn.transform;

        // Calculate spawn position based on direction, without modifying the transform
        Vector3 offset = spawnTransform.localPosition;
        offset.x = Mathf.Abs(offset.x) * Mathf.Sign(m_Antagonist.m_Direction.x);
        Vector3 spawn = spawnTransform.parent.TransformPoint(offset);

        Projectile proj = m_PoolManager.Get();
        proj.Init(m_Antagonist);
        proj.GetComponent<Hitbox>().Initialize(new DamageEffect(data.damage));
        proj.SpriteRenderer.flipX = Mathf.Sign(m_Antagonist.m_Direction.x) > 0;

        // Set projectile position and velocity
        proj.transform.SetPositionAndRotation(spawn, Quaternion.identity);
        proj.Velocity = new Vector2(Mathf.Sign(m_Antagonist.m_Direction.x) * data.speed, 0);
    }
}