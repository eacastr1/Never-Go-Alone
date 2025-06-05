using UnityEngine;

/// <summary>
/// AbilityManager is a singleton that manages abilities in the game.
/// It ensures that only one instance exists throughout the game and persists across scenes.
/// It is reponsible and tied to handling ability activation, deactivation, and management.
/// </summary>

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}