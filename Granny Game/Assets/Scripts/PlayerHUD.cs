using TMPro; // Instead of UnityEngine.UI
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text strengthText;
   // public TMP_Text helmetText;
    void Start()
    {
        Debug.Log("PlayerHUD script is running ✅");

        healthText.text = "Health: " + PlayerStats.currentHealth;
        strengthText.text = "Strength: " + PlayerStats.strength;
    }


    void Update()
    {
        healthText.text = "Health: " + PlayerStats.currentHealth + " / " + PlayerStats.maxHealth;
        strengthText.text = "Strength: " + PlayerStats.strength + " / " + PlayerStats.strengthRequiredToBreak;
      //  helmetText.text = PlayerStats.hasHelmet ? "Helmet: " + PlayerStats.selectedHelmet : "Helmet: None";
    }
}