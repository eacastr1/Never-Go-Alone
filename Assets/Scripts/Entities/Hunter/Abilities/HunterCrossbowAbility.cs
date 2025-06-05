using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class HunterCrossbowAbility : ProtagAbility
{
    private Projectile m_Prefab;
    private ProjectilePoolManager m_PoolManager;

    public HunterCrossbowAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        m_Prefab = data.prefab.GetComponent<Projectile>();
        m_PoolManager = new ProjectilePoolManager(m_Prefab);
    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown  

        m_Protagonist.m_Performing = true; // Set performing to true to prevent other actions
        AbilityManager.Instance.StartCoroutine(Fire());
    }

    protected override void Deactivate()
    {
        Debug.Log("Hunter Crossbow Ability Deactivated");
    }

    private IEnumerator Fire()
    {
        m_Protagonist.Animator.SetTrigger("Ability One");

        yield return new WaitForSeconds(0.1f); // Wait for the animation to finish

        Transform spawnTransform = ((Hunter)m_Protagonist).BoltArrowSpawn.transform;

        // Flip spawn position relative to player direction
        Vector3 localPos = spawnTransform.localPosition;
        localPos.x = Mathf.Abs(localPos.x) * Mathf.Sign(m_Protagonist.Direction);
        spawnTransform.localPosition = localPos;

        Vector2 spawn = spawnTransform.position;

        Projectile proj = m_PoolManager.Get();
        proj.Init(m_Protagonist);

        proj.GetComponent<Hitbox>().Initialize(new DamageEffect(data.damage));

        
        proj.SpriteRenderer.flipX = Mathf.Sign(m_Protagonist.Direction) < 0;

        // Set projectile position and velocity
        proj.transform.SetPositionAndRotation(spawn, Quaternion.identity);
        proj.Velocity = new Vector2(m_Protagonist.Direction * data.speed, 0);

        m_Protagonist.m_Performing = false; // Reset performing state after the ability is activated

        Debug.Log("Hunter Crossbow Ability Activated");
    }
}