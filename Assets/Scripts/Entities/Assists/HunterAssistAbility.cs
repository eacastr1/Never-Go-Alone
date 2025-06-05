using System.Collections;
using UnityEngine;

public class HunterAssistAbility : Ability
{
    private GameObject m_Hunter;

    public HunterAssistAbility(Entity entity, AbilityData data) : base(entity, data)
    {
        m_Hunter = data.prefab;
    }

    protected override void Activate()
    {
        if (Cooldown()) return;

        AbilityManager.Instance.StartCoroutine(HunterAssist());
    }

    protected override void Deactivate()
    {

    }

    private IEnumerator HunterAssist()
    {

        GameObject prefab = null;

        if (owner.Direction > 0)
        {
            Vector2 offset = new Vector2(owner.transform.position.x - 0.5f, owner.transform.position.y);
            // spawn with negative offset
            prefab = GameObject.Instantiate(m_Hunter, offset, Quaternion.identity);
            // flip in the positive direction
            Vector3 scale = prefab.transform.localScale;
            scale.x = 1; // Flip horizontally
            prefab.transform.localScale = scale;
        }
        else if (owner.Direction < 0)
        {
            Vector2 offset = new Vector2(owner.transform.position.x + 0.5f, owner.transform.position.y);
            // spawn with positive offset
            prefab = GameObject.Instantiate(m_Hunter, offset, Quaternion.identity);
            // flip in the negative direction
            Vector3 scale = prefab.transform.localScale;
            scale.x = -1; // Flip horizontally
            prefab.transform.localScale = scale;
        }

        yield return new WaitForSeconds(0.34f);

        Vector2 originalPosition = prefab.transform.position;
        Vector2 targetPosition = new Vector2(
            originalPosition.x + (data.distance * owner.Direction),
            originalPosition.y
        );

        float maxDuration = 1f;
        float elapsedTime = 0f;


        Rigidbody2D rigidbody2D = prefab.GetComponentInChildren<Rigidbody2D>();
        Hitbox hitbox = prefab.GetComponentInChildren<Hitbox>();

        hitbox.Initialize(new DamageEffect(data.damage));
        hitbox.SetTag("Enemy");

        if (rigidbody2D == null)
        {
            Debug.Log("Assist error");
        }

        // move character quickly a certain distance
        while (Vector2.Distance(rigidbody2D.position, targetPosition) > 0.05f
                && elapsedTime < maxDuration)
        {
            Vector2 newPos = Vector2.MoveTowards(
                rigidbody2D.position,
                targetPosition,
                data.speed * Time.fixedDeltaTime
            );

            rigidbody2D.MovePosition(newPos);

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.34f);

        GameObject.Destroy(prefab);
        // and that's that?
    }
}