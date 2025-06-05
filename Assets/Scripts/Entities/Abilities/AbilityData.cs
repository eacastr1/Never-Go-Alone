using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/System/AbilityData")]
public class AbilityData : ScriptableObject
{
    public GameObject prefab;
    public AbilityNames abilityName;
    public float cooldown; // how long until this ability can be activated again
    public float recovery; // recovery time
    public float damage;
    public float speed; // speed of the ability
    public float duration; // how long the ability lasts
    public float distance;
}
