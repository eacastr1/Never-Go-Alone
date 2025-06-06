using System.Collections;
using UnityEngine;

public class WarriorChainAbility : ProtagAbility
{
    private GameObject m_Chain;
    private Transform m_Spawn;
    public WarriorChainAbility(Protagonist protagonist, AbilityData data) : base(protagonist, data)
    {
        m_Chain = data.prefab;
        m_Spawn = ((Warrior)protagonist).ChainSpawn.transform;
    }

    protected override void Activate()
    {
        if (Cooldown()) return;

        AbilityManager.Instance.StartCoroutine(Chain());
        // m_Protagonist.m_Rooted = true;
    }

    protected override void Deactivate()
    {

    }

    private IEnumerator Chain()
    {
        m_Protagonist.Animator.SetTrigger("Charge");
        m_Protagonist.m_Floating = true;
        m_Protagonist.m_Performing = true;
        GameObject chain = GameObject.Instantiate(m_Chain, m_Spawn);

        Hitbox hitbox = chain.GetComponent<Hitbox>();
        hitbox.Initialize(new DamageEffect(15f), new GrappleEffect(m_Protagonist, m_Protagonist.transform.position, data.speed, 0.4f));
        hitbox.SetTag("Enemy");

        if (m_Protagonist.Direction > 0)
        {
            m_Spawn.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            m_Spawn.rotation = Quaternion.Euler(0, 0, 180);
        }

        yield return new WaitForSeconds(data.duration);

        GameObject.Destroy(chain);
        m_Protagonist.m_Performing = false;

        yield return new WaitForSeconds(data.duration);
        // m_Protagonist.m_Rooted = false;
        m_Protagonist.m_Floating = false;
    }
}