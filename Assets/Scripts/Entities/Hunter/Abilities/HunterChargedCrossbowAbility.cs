using UnityEngine;
using UnityEngine.Pool;
using UnityHFSM;
using System.Collections;

public class HunterChargedCrossbowAbility : ProtagAbility
{

    private Projectile m_Prefab;
    private ProjectilePoolManager m_PoolManager;

    public HunterChargedCrossbowAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        m_Prefab = data.prefab.GetComponent<Projectile>();
        m_PoolManager = new ProjectilePoolManager(m_Prefab);
    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown
        
        m_Protagonist.m_Performing = true; // Set performing to true to prevent other actions
        m_Protagonist.Animator.SetTrigger("Ability Two");
        m_Protagonist.m_Rooted = true; // Root the protagonist during the charge animation
        AbilityManager.Instance.StartCoroutine(Fire());
    }

    protected override void Deactivate()
    {
        // throw new System.NotImplementedException();
    }

    private IEnumerator Fire()
    {

        yield return new WaitForSeconds(0.4f); // Wait for the animation to finish

        Transform spawnTransform = ((Hunter)m_Protagonist).BoltArrowSpawn.transform;

        // Flip spawn position relative to player direction
        Vector3 localPos = spawnTransform.localPosition;
        localPos.x = Mathf.Abs(localPos.x) * Mathf.Sign(m_Protagonist.Direction);
        spawnTransform.localPosition = localPos;

        Vector2 spawn = spawnTransform.position;

        Projectile proj = m_PoolManager.Get();
        proj.Init(m_Protagonist);
        proj.SpriteRenderer.flipX = Mathf.Sign(m_Protagonist.Direction) < 0;

        proj.GetComponent<Hitbox>().Initialize(new DamageEffect(data.damage));

        // Set projectile position and velocity
        proj.transform.SetPositionAndRotation(spawn, Quaternion.identity);
        proj.Velocity = new Vector2(m_Protagonist.Direction * data.speed, 0);

        yield return new WaitForSeconds(0.25f);

        m_Protagonist.m_Rooted = false; // Unroot the protagonist after firing
        m_Protagonist.m_Performing = false; // Reset performing state after the ability is activated

        Debug.Log("Hunter Crossbow Ability Activated");
    }
}