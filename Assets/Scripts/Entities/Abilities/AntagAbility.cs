public abstract class AntagAbility : Ability
{
    protected Antagonist m_Antagonist;

    public AntagAbility(Antagonist antagonist, AbilityData data) : base(antagonist, data)
    {
        m_Antagonist = antagonist;
    }
    
}