using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int boostedMaxHealth = 150;

    private bool hasApple = false;
    private bool hasUsedApple = false;

    public HealthAppleUI healthUI; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (hasApple && !hasUsedApple && Input.GetKeyDown(KeyCode.H))
        {
            EatApple();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log("Player lost " + damage + " health. Current health: " + currentHealth);
    }

    public void UnlockApple()
    {
        hasApple = true;
    }

    public void EatApple()
    {
        maxHealth = boostedMaxHealth;
        currentHealth = maxHealth;
        hasUsedApple = true;

        Debug.Log("🍎 Apple eaten! Health restored to " + currentHealth + ". Max health is now " + maxHealth);

        if (healthUI != null)
            healthUI.HideUseHint(); 
    }
}