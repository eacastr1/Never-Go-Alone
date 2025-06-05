using UnityEngine;

public class Bomb : MonoBehaviour
{
    private DamageEffect m_DamageEffect;
    private KnockbackEffect m_KnockbackEffect;


    void Update()
    {

    }

    private void Explode()
    {
        // Create the damage effect
        // m_DamageEffect = new DamageEffect(10f); // Example damage value
        // Create the knockback effect
        // m_KnockbackEffect = new KnockbackEffect(transform.position, transform.position, null); // Example distance and position

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 6f);

        Destroy(gameObject); // Destroy the bomb after explosion
    }
}