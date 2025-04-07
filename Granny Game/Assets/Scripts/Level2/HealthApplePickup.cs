using UnityEngine;

public class HealthApplePickup : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private PlayerHealth playerHealth;
    private HealthAppleUI healthUI;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerHealth != null)
            {
                playerHealth.UnlockApple();

                if (healthUI != null)
                {
                    healthUI.ShowPickupPrompt(false);
                    healthUI.ShowUseHint();     
                }

                Destroy(gameObject); 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            healthUI = FindObjectOfType<HealthAppleUI>(); // ✅ fixed this line
            isPlayerNearby = true;

            if (healthUI != null)
                healthUI.ShowPickupPrompt(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (healthUI != null)
                healthUI.ShowPickupPrompt(false); 
        }
    }
}