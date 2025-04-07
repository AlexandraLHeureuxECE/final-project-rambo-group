using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddStrength(int amount)
    {
        PlayerStats.strength += amount;
        Debug.Log($"[STRENGTH] Player strength is now: {PlayerStats.strength}");
    }
}