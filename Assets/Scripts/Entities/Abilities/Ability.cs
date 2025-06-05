using System.Collections;
using UnityEngine;

public abstract class Ability
{
    protected Entity owner;
    protected AbilityData data;

    public Ability(Entity owner, AbilityData data)
    {
        this.owner = owner;
        this.data = data;
    }

    public void TryActivate()
    {
        Activate();
    }

    public void TryDeactivate()
    {
        Deactivate();
    }

    protected bool Cooldown()
    {
        if (CooldownManager.Instance.IsOnCooldown($"{owner.name}: {data.abilityName}"))
        {
            // Debug.Log("Ability is on cooldown");
            return true;
        }
        CooldownManager.Instance.AddCooldown($"{owner.name}: {data.abilityName}", data.cooldown);
        return false;
    }

    protected abstract void Activate();
    protected abstract void Deactivate();

    
}
