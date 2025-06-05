using UnityEngine;
public static class AbilityFactory
{
    public static Ability CreateAbility(AbilityData data, Entity owner)
    {
        AbilityNames abilityName = data.abilityName;

        switch (abilityName)
        {
            // ARCHER ABILITIES
            case AbilityNames.HUNTER_CROSSBOW:
                return new HunterCrossbowAbility((Protagonist)owner, data);
            case AbilityNames.HUNTER_CHARGED_CROSSBOW:
                return new HunterChargedCrossbowAbility((Protagonist)owner, data);
            case AbilityNames.HUNTER_ROLL:
                return new HunterRollAbility((Protagonist)owner, data);
            case AbilityNames.HUNTER_BOMB:
                return new HunterBombAbility((Protagonist)owner, data);
            case AbilityNames.HUNTER_SWORD_ATTACK:
                return new HunterSwordAttackAbility((Protagonist)owner, data);
            case AbilityNames.HUNTER_SWORD_AIR_ATTACK:
                return new HunterSwordAirAttackAbility((Protagonist)owner, data);

            // CULTIST ABILITIES
            case AbilityNames.CULTIST_FIREBALL:
                return new CultistFireBallAbility((Antagonist)owner, data);
            case AbilityNames.CULTIST_TWISTED_TENTACLES:
                return new CultistTentaclesAbility((Antagonist)owner, data);

            // MAGE ABILITIES
            case AbilityNames.MAGE_EAGLE:
                // return new MageEagleAbility((Protagonist) owner, data);
                Debug.LogError("Mage Eagle ability not implemented yet.");
                return null;
            case AbilityNames.MAGE_WEREBEAR:
                // return new MageWerebearAbility((Protagonist) owner, data);
                Debug.LogError("Mage Werebear ability not implemented yet.");
                return null;
            case AbilityNames.MAGE_ZOLTRAAK:
                // return new MageZoltraakAbility((Protagonist) owner, data);
                Debug.LogError("Mage Zoltraak ability not implemented yet.");
                return null;

            // WARRIOR ABILITIES
            case AbilityNames.WARRIOR_CHARGE:
                return new WarriorChargeAbility((Protagonist)owner, data);
            case AbilityNames.WARRIOR_SWORD_SLASH_DOWN:
                return new WarriorSlashDownAbility((Protagonist)owner, data);
            case AbilityNames.WARRIOR_SWORD_SLASH_UP:
                return new WarriorSlashUpAbility((Protagonist)owner, data);
            case AbilityNames.WARRIOR_CHAIN:
                return new WarriorChainAbility((Protagonist)owner, data);
            case AbilityNames.WARRIOR_JUMP_SLAM:
                return new WarriorJumpSlamAbility((Protagonist)owner, data);

            // ASSIST ABILITIES
            case AbilityNames.HUNTER_DASH_ATTACK_ASSIST:
                return new HunterAssistAbility(owner, data);
                
            // Add more cases for other abilities as needed
            default:
                Debug.LogError($"Ability {abilityName} not implemented.");
                return null;
        }
    }
}