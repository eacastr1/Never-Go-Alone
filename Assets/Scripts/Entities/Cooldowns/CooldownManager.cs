using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance { get; private set; }
    public Dictionary<string, Cooldown> CooldownDictionary;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);


        CooldownDictionary = new Dictionary<string, Cooldown>();
    }

    public void AddCooldown(string abilityName, float cooldownDuration)
    {

        if (!CooldownDictionary.ContainsKey(abilityName))
        {
            CooldownDictionary[abilityName] = new Cooldown(cooldownDuration);
        }
        else
        {
            CooldownDictionary[abilityName].Reset(cooldownDuration);
        }

        StartCoroutine(CooldownDictionary[abilityName].StartCooldown());
    }

    public bool IsOnCooldown(string abilityName)
    {
        if (CooldownDictionary.ContainsKey(abilityName))
        {
            return CooldownDictionary[abilityName].IsOnCooldown();
        }
        return false;
    }
}