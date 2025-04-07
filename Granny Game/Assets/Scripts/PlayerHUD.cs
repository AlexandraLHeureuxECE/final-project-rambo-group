using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider strengthSlider;

    [Header("Text Labels")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI strengthText;

    void Start()
    {
        // Set visual bar limits
        healthSlider.minValue = 0;
        healthSlider.maxValue = 100;

        strengthSlider.minValue = 0;
        strengthSlider.maxValue = 100; // Always 100 visually
    }

    void Update()
    {
        UpdateHealth(PlayerStats.currentHealth, PlayerStats.maxHealth);
        UpdateStrength(PlayerStats.strength);
    }

    public void UpdateHealth(int current, int max)
    {
        healthSlider.value = (float)current / max * 100f;

        if (healthText != null)
            healthText.text = $"Health: {current}/{max}";
    }

    public void UpdateStrength(int current)
    {
        strengthSlider.value = Mathf.Clamp(current, 0, 100); // bar max 100
        if (strengthText != null)
            strengthText.text = $"Strength: {current}/100";
    }
}