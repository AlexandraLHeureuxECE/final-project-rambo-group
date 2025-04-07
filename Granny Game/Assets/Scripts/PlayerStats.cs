using UnityEngine;

public static class PlayerStats
{
    public static bool hasStrength = false;
    public static bool hasKey = false;
    public static bool hasHelmet = false;
    public static string selectedHelmet = "";
    public static int strength = 0; 
    public static int strengthRequiredToBreak = 50;


    public static bool hasArmor = false;
    public static float speedMultiplier = 1f;
    public static int maxHealth = 100;
    public static int currentHealth = 100;
}
