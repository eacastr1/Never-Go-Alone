using System.Collections;
using UnityEngine;

public class Cooldown
{
    private float m_Duration;
    public Cooldown(float duration)
    {
        m_Duration = duration;
    }

    public bool IsOnCooldown()
    {
        return m_Duration > 0;
    }

    public void Reset(float duration)
    {
        m_Duration = duration;
    }

    public IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(m_Duration);
        m_Duration = 0f; // Reset cooldown after the duration
    }
}