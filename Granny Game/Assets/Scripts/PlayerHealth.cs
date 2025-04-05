using UnityEngine;
using Microlight.MicroBar;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("MicroBar UI")]
    [SerializeField] private MicroBarUI healthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBarUI != null)
        {
            healthBarUI.Initialize();
            healthBarUI.SetNewMaxHP(maxHealth);
            healthBarUI.UpdateBar(currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateBar(currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateBar(currentHealth);
    }
}