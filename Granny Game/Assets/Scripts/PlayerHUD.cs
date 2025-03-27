using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text healthText;
    public Text strengthText;

    void Update()
    {
        healthText.text = "Health: " + PlayerStats.currentHealth;
        strengthText.text = "Strength: " + PlayerStats.strength + " / " + PlayerStats.strengthRequiredToBreak;
    }
}
