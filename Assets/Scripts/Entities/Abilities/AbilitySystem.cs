using UnityEngine;
using System.Collections.Generic;
public class AbilitySystem : MonoBehaviour
{
    // private List<AbilityData> m_Abilities;
    private Entity m_Entity;
    private Dictionary<AbilityNames, Ability> m_AbilitiesDictionary;

    public void Awake()
    {
        m_Entity = GetComponent<Entity>();
        // m_Abilities = new List<AbilityData>();
    }

    public void InitAbilities(List<AbilityData> abilities)
    {
        m_AbilitiesDictionary = new Dictionary<AbilityNames, Ability>();
        foreach (AbilityData data in abilities)
        {
            m_AbilitiesDictionary.Add(data.abilityName, AbilityFactory.CreateAbility(data, m_Entity));
        }
    }

    public Ability GetAbility(AbilityNames abilityName)
    {
        return m_AbilitiesDictionary[abilityName];
    }
}