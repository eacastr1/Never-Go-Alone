using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/EntityData")]
public class EntityData : ScriptableObject
{
    public GameObject Entity;
    public EntityNames Name;
    public List<AbilityData> Abilities;
    public float Health;
    public float Speed;

    // more to come...?

    public void AddAbility(AbilityData ability)
    {
        Abilities.Add(ability);
    }
}