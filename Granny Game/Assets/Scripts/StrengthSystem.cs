using UnityEngine;

public class StrengthSystem
{
    private int strength;
    private int maxStrength;

    public StrengthSystem(int maxStrength)
    {
        this.maxStrength = maxStrength;
        strength = 0;
    }

    public int GetStrength() => strength;

    public float GetNormalizedStrength() => (float)strength / maxStrength;

    public void SetStrength(int amount)
    {
        strength = Mathf.Clamp(amount, 0, maxStrength);
    }

    public void AddStrength(int amount)
    {
        strength = Mathf.Clamp(strength + amount, 0, maxStrength);
    }

    public bool HasEnough(int required) => strength >= required;
}