using System.Collections;
using UnityEngine;

public class HunterBombAbility : ProtagAbility
{
    private GameObject m_BombPrefab;
    private Transform m_BombSpawn;

    public HunterBombAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        // Initialize the bomb ability with the protagonist and ability data
        m_BombPrefab = data.prefab;
        m_BombSpawn = ((Hunter)protagonist).BombSpawn.transform;
        if (m_BombPrefab == null)
        {
            Debug.LogError("Bomb prefab is not assigned in AbilityData.");
        }
    }

    protected override void Activate()
    {
        if (Cooldown()) return; // Check if the ability is on cooldown
        m_Protagonist.m_Performing = true; // Set performing to true to prevent other actions

        AbilityManager.Instance.StartCoroutine(Bomb());
    }

    protected override void Deactivate()
    {
        // Logic for deactivating the bomb ability can be added here if needed
    }

    private IEnumerator Bomb()
    {
        m_Protagonist.Animator.SetTrigger("Ability Four");
        yield return new WaitForSeconds(0.4f);
        GameObject bomb = Object.Instantiate(m_BombPrefab, m_BombSpawn.position, Quaternion.identity);
        if (bomb == null)
        {
            Debug.LogError("Failed to instantiate bomb prefab.");
            yield break;
        }

        m_Protagonist.m_Performing = false; // Reset performing state after the ability is activated

        Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();
        if (bombRigidbody == null)
        {
            Debug.LogError("Bomb prefab does not have a Rigidbody2D component.");
            yield break;
        }
        Vector2 direction = new Vector2(m_Protagonist.Direction, 0.5f);

        bombRigidbody.AddForce(direction * data.speed, ForceMode2D.Impulse);

        float friction = 0.98f; // Adjust closer to 1 for less friction, lower for more

        while (bombRigidbody.linearVelocity.magnitude > 0.1f)
        {
            bombRigidbody.linearVelocity *= friction;
            yield return new WaitForFixedUpdate(); // Wait until the bomb stops moving
        }
    }
}