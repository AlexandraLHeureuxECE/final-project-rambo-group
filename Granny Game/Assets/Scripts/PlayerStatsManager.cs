using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    private StrengthSystem strengthSystem;

    private void Awake()
    {
        Instance = this;
        strengthSystem = new StrengthSystem(100);
    }

    public void AddStrength(int amount)
    {
        strengthSystem.AddStrength(amount);
        Debug.Log("Strength increased! Now: " + strengthSystem.GetStrength());
    }

    public StrengthSystem GetStrengthSystem()
    {
        return strengthSystem;
    }
}